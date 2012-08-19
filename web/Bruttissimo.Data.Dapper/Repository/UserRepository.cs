using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Bruttissimo.Data.Dapper
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        private readonly IDbConnection connection;
        private readonly HashProvider hashProvider;

        public UserRepository(IDbConnection connection, HashProvider hashProvider)
            : base(connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (hashProvider == null)
            {
                throw new ArgumentNullException("hashProvider");
            }
            this.connection = connection;
            this.hashProvider = hashProvider;
        }

        #region Get

        public User GetByEmail(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            const string sql = @"
                SELECT [User].*
                FROM [User]
                WHERE [User].[Email] = @email
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new {email});
            User user = result.FirstOrDefault();
            return user;
        }

        public User GetByOpenId(string openId)
        {
            if (openId == null)
            {
                throw new ArgumentNullException("openId");
            }
            const string sql = @"
				SELECT [User].* FROM [User]
				INNER JOIN [UserConnection]
				ON [User].[Id] = [UserConnection].[UserId]
				WHERE [UserConnection].[OpenId] = @openId
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new {openId});

            User user = result.FirstOrDefault();
            return user;
        }

        public User GetByFacebookGraphId(string facebookId)
        {
            if (facebookId == null)
            {
                throw new ArgumentNullException("facebookId");
            }
            const string sql = @"
				SELECT [User].* FROM [User]
				INNER JOIN [UserConnection]
				ON [User].[Id] = [UserConnection].[UserId]
				WHERE [UserConnection].[FacebookId] = @facebookId
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new {facebookId});

            User user = result.FirstOrDefault();
            return user;
        }

        public User GetByTwitterId(string twitterId)
        {
            if (twitterId == null)
            {
                throw new ArgumentNullException("twitterId");
            }
            const string sql = @"
				SELECT [User].* FROM [User]
				INNER JOIN [UserConnection]
				ON [User].[Id] = [UserConnection].[UserId]
				WHERE [UserConnection].[TwitterId] = @twitterId
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new {twitterId});

            User user = result.FirstOrDefault();
            return user;
        }

        #endregion

        #region Create

        public User CreateWithCredentials(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            User user = InternalCreate(email, password, null);
            return user;
        }

        public User CreateWithOpenId(string openId, string email, string displayName)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                User user = InternalCreate(email, null, displayName, transaction);
                AddOpenIdConnection(user, openId, transaction);
                transaction.Commit();
                return user;
            }
        }

        public User CreateWithFacebook(string facebookId, string accessToken, string email, string displayName)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                User user = InternalCreate(email, null, displayName, transaction);
                AddFacebookConnection(user, facebookId, accessToken, transaction);
                transaction.Commit();
                return user;
            }
        }

        public User CreateWithTwitter(string twitterId, string displayName)
        {
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                User user = InternalCreate(null, null, displayName, transaction);
                AddTwitterConnection(user, twitterId, transaction);
                transaction.Commit();
                return user;
            }
        }

        #endregion

        #region AddConnection

        public UserConnection AddOpenIdConnection(User user, string openId)
        {
            return AddOpenIdConnection(user, openId, null);
        }

        public UserConnection AddOpenIdConnection(User user, string openId, IDbTransaction transaction)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (openId == null)
            {
                throw new ArgumentNullException("openId");
            }
            UserConnection userConnection = new UserConnection
            {
                UserId = user.Id,
                OpenId = openId
            };
            connection.Insert(userConnection, transaction);
            return userConnection;
        }

        public UserConnection AddFacebookConnection(User user, string facebookId, string accessToken)
        {
            return AddFacebookConnection(user, facebookId, accessToken, null);
        }

        public UserConnection AddFacebookConnection(User user, string facebookId, string accessToken, IDbTransaction transaction)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (facebookId == null)
            {
                throw new ArgumentNullException("facebookId");
            }
            UserConnection userConnection = new UserConnection
            {
                UserId = user.Id,
                FacebookId = facebookId,
                FacebookAccessToken = accessToken
            };
            connection.Insert(userConnection, transaction);
            return userConnection;
        }

        public UserConnection AddTwitterConnection(User user, string twitterId)
        {
            return AddTwitterConnection(user, twitterId, null);
        }

        public UserConnection AddTwitterConnection(User user, string twitterId, IDbTransaction transaction)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (twitterId == null)
            {
                throw new ArgumentNullException("twitterId");
            }
            UserConnection userConnection = new UserConnection
            {
                UserId = user.Id,
                TwitterId = twitterId
            };
            connection.Insert(userConnection, transaction);
            return userConnection;
        }

        #endregion

        public bool IsInRoleOrHasRight(User user, string roleOrRight)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (roleOrRight == null)
            {
                throw new ArgumentNullException("roleOrRight");
            }
            if (user.Role == null)
            {
                user.Role = GetRoleDetails(user); // load lazily.
            }
            // Shim this finer grained right-based schema into the ASP.NET role-based framework.
            if (user.Role.Name.InsensitiveEquals(roleOrRight))
            {
                return true;
            }
            return user.Role.Rights.Any(right => right.Name.InsensitiveEquals(roleOrRight));
        }

        public bool AreMatchingPasswords(string password, string databaseHash)
        {
            string hash = InternalPasswordHash(password);
            return hash == databaseHash;
        }

        #region Internals

        private User InternalCreate(string email, string password, string displayName, IDbTransaction transaction = null)
        {
            User user = new User
            {
                Email = email,
                DisplayName = GetDisplayName(email, displayName),
                Password = password == null ? null : InternalPasswordHash(password),
                Created = DateTime.UtcNow,
                RoleId = GetRoleId(Roles.Regular, transaction)
            };
            connection.Insert(user, transaction);
            return user;
        }

        private long GetRoleId(string name, IDbTransaction transaction = null)
        {
            const string sql = @"
                SELECT [Role].[Id]
                FROM [Role]
                WHERE [Role].[Name] = @name
            ";
            long role = connection.Query<long>(sql, new {name}, transaction).First();
            return role;
        }

        private Role GetRoleDetails(User user)
        {
            Role role = connection.Get<Role>(user.RoleId);
            const string sql = @"
				SELECT [R].[Id], [R].[Name] FROM [Right] [R]
				INNER JOIN [RoleRight] [RR] ON [R].[Id] = [RR].[RightId]
				INNER JOIN [Role] ON [RR].[RoleId] = @roleId
            ";
            IEnumerable<Right> rights = connection.Query<Right>(sql, new {roleId = user.RoleId});

            role.Rights = rights;
            return role;
        }

        private string GetDisplayName(string email, string displayName)
        {
            if (displayName != null)
            {
                return displayName;
            }
            else if (email != null)
            {
                return email.Split('@')[0];
            }
            else
            {
                return null;
            }
        }

        internal string InternalPasswordHash(string password)
        {
            return hashProvider.ComputeAsString(password);
        }

        #endregion
    }
}
