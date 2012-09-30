using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Static;
using Bruttissimo.Common.Utility;
using Bruttissimo.Domain.Entity.Constants;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Dapper;

namespace Bruttissimo.Data.Dapper.Repository
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        private readonly IDbConnection connection;
        private readonly HashProvider hashProvider;

        public UserRepository(IDbConnection connection, HashProvider hashProvider)
            : base(connection)
        {
            Ensure.That(() => connection).IsNotNull();
            Ensure.That(() => hashProvider).IsNotNull();

            this.connection = connection;
            this.hashProvider = hashProvider;
        }

        #region Get

        public User GetByEmail(string email)
        {
            Ensure.That(() => email).IsNotNull();

            const string sql = @"
                SELECT [User].*
                FROM [User]
                WHERE [User].[Email] = @email
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new { email });
            User user = result.FirstOrDefault();
            return user;
        }

        public User GetByOpenId(string openId)
        {
            Ensure.That(() => openId).IsNotNull();

            const string sql = @"
				SELECT [User].* FROM [User]
				INNER JOIN [UserConnection]
				ON [User].[Id] = [UserConnection].[UserId]
				WHERE [UserConnection].[OpenId] = @openId
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new { openId });

            User user = result.FirstOrDefault();
            return user;
        }

        public User GetByFacebookGraphId(string facebookId)
        {
            Ensure.That(() => facebookId).IsNotNull();

            const string sql = @"
				SELECT [User].* FROM [User]
				INNER JOIN [UserConnection]
				ON [User].[Id] = [UserConnection].[UserId]
				WHERE [UserConnection].[FacebookId] = @facebookId
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new { facebookId });

            User user = result.FirstOrDefault();
            return user;
        }

        public User GetByTwitterId(string twitterId)
        {
            Ensure.That(() => twitterId).IsNotNull();

            const string sql = @"
				SELECT [User].* FROM [User]
				INNER JOIN [UserConnection]
				ON [User].[Id] = [UserConnection].[UserId]
				WHERE [UserConnection].[TwitterId] = @twitterId
            ";
            IEnumerable<User> result = connection.Query<User>(sql, new { twitterId });

            User user = result.FirstOrDefault();
            return user;
        }

        #endregion

        #region Create

        public User CreateWithCredentials(string email, string password)
        {
            Ensure.That(() => email).IsNotNull();
            Ensure.That(() => password).IsNotNull();

            User user = InternalCreate(email, password, null);
            return user;
        }

        public User CreateWithOpenId(string openId, string email, string displayName)
        {
            Ensure.That(() => email).IsNotNull();

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
            Ensure.That(() => email).IsNotNull();

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

        public string GetFacebookAccessToken(User user)
        {
            const string sql = @"
                SELECT TOP 1 [UC].[FacebookAccessToken]
                FROM [UserConnection] [UC]
                WHERE [UC].[UserId] = @userId
            "; // TOP 1 is temporary, might change if we start supporting multiple accounts for each provider.
            string accessToken = connection.Query<string>(sql, new { userId = user.Id }).FirstOrDefault();
            return accessToken;
        }

        public void RevokeFacebookAccessToken(string accessToken)
        {
            const string sql = @"
                UPDATE [UserConnection]
                SET [FacebookAccessToken] = NULL
                WHERE [FacebookAccessToken] = @accessToken
            ";
            connection.Query(sql, new { accessToken });
        }

        public UserConnection AddOpenIdConnection(User user, string openId, IDbTransaction transaction)
        {
            Ensure.That(() => user).IsNotNull();
            Ensure.That(() => openId).IsNotNull();

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
            Ensure.That(() => user).IsNotNull();
            Ensure.That(() => facebookId).IsNotNull();

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
            Ensure.That(() => user).IsNotNull();
            Ensure.That(() => twitterId).IsNotNull();

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
            Ensure.That(() => user).IsNotNull();
            Ensure.That(() => roleOrRight).IsNotNull();
            
            if (user.Role == null)
            {
                user.Role = GetRoleDetails(user); // load lazily.
            }

            string name = roleOrRight.Trim(); // avoid issues with AuthorizeAttribute role list being "RoleA, RoleB".

            if (user.Role.Name.InsensitiveEquals(name)) // shim this finer grained right-based schema into the ASP.NET role-based framework.
            {
                return true;
            }
            return user.Role.Rights.Any(right => right.Name.InsensitiveEquals(name));
        }

        public bool AreMatchingPasswords(string password, string databaseHash)
        {
            string hash = InternalPasswordHash(password);
            return hash == databaseHash;
        }

        #region Internals

        private User InternalCreate(string email, string password, string displayName, IDbTransaction transaction = null)
        {
            UserSettings settings = new UserSettings
            {
                TimeZone = Config.Defaults.TimeZone
            };
            connection.Insert(settings, transaction);
            User user = new User
            {
                RoleId = GetRoleId(Roles.Regular, transaction),
                Email = email,
                DisplayName = GetDisplayName(email, displayName),
                Password = password == null ? null : InternalPasswordHash(password),
                Created = DateTime.UtcNow,
                UserSettingsId = settings.Id
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
            long role = connection.Query<long>(sql, new { name }, transaction).First();
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
            IEnumerable<Right> rights = connection.Query<Right>(sql, new { roleId = user.RoleId });

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
