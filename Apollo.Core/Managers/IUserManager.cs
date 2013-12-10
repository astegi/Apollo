using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.Core.Attributes;

namespace Apollo.Core.Managers
{
    [ProxyContract]
    public interface IUserManager
    {
        /// <summary>
        /// Lekérdezi az aktuális felhasználót
        /// </summary>
        /// <returns></returns>
        [ProxyMember]
        string GetCurrentUser();

        /// <summary>
        /// Lekérdezi az aktuális felhasználó csoporttagságait
        /// </summary>
        /// <returns></returns>
        [ProxyMember]
        List<string> GetMemberships();
    }
}
