using System.Collections.Generic;
using Apollo.Core.Authentication;
using Apollo.Core.Managers;
using System.Security.Permissions;
using Apollo.Core;
using System;
using Apollo.Business.Attributes;

namespace Apollo.Business
{
    [ProxyImplementation]
    public class UserManager : MarshalByRefObject, IUserManager
    {
        [PrincipalPermission(SecurityAction.Demand, Role=Constants.ROLE_USER)]
        public string GetCurrentUser()
        {
            return User.CurrentUser.Name;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Constants.ROLE_USER)]
        public List<string> GetMemberships()
        {
            return User.CurrentUser.Groups;
        }
    }
}
