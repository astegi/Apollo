using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.Core.Managers;

namespace Apollo.Core
{
    /// <summary>
    /// Külső proxy osztály
    /// </summary>
    public static class Proxy
    {
        static Proxy()
        {
            if (Constants.USE_PROXYCLASS) ServiceProvider.Instance.FillProxyByProxyClass();
            else ServiceProvider.Instance.FillProxy();
        }

        public static void Initialize()
        {
        }

        /// <summary>
        /// Felhasználó kezelés
        /// </summary>
        public static IUserManager UserManager;

        /// <summary>
        /// Eseménynapló kezelés
        /// </summary>
        public static ITraceManager TraceManager;

    }
}
