using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    public class MiniRoleProvider : RoleProvider
    {
        private readonly IUserRepository userRepository;

        public MiniRoleProvider(IUserRepository userRepository)
        {
            Ensure.That(() => userRepository).IsNotNull();

            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="email">The user email to search for.</param><param name="roleOrRight">The role to search in.</param>
        public override bool IsUserInRole(string email, string roleOrRight)
        {
            User user = userRepository.GetByEmail(email);
            if (user == null)
            {
                return false;
            }
            return userRepository.IsInRoleOrHasRight(user, roleOrRight);
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        /// <param name="email">The user email to return a list of roles for.</param>
        public override string[] GetRolesForUser(string email)
        {
            User user = userRepository.GetByEmail(email);
            if (user == null)
            {
                return new string[0];
            }
            IList<string> roles = user.Role.Rights.Select(r => r.Name).ToList();
            roles.Add(user.Role.Name);
            return roles.ToArray();
        }

        #region Overrides of RoleProvider not implemented

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
}
