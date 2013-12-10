using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Core
{
    public class Constants
    {
        public const string ROLE_ADMINISTRATOR = "Családfő";
        public const string ROLE_USER = "Családtag";

        public const string EVENTLOG_NAME = "ApolloLog";

        public static bool USE_PROXYCLASS = true;
        /// <summary>
        /// 
        /// </summary>
        public const int TIMEOUT = 1000;


        /// <summary>
        /// Success Audit
        /// </summary>
        public const string TRACE_EVENT_SUCCESS = "Success";
        /// <summary>
        /// Failure Audit
        /// </summary>
        public const string TRACE_EVENT_FAILURE = "Failure";
        /// <summary>
        /// Error
        /// </summary>
        public const string TRACE_EVENT_ERROR = "Error";
        /// <summary>
        /// Warning
        /// </summary>
        public const string TRACE_EVENT_WARNING = "Warning";
        /// <summary>
        /// Information
        /// </summary>
        public const string TRACE_EVENT_INFORMATION = "Information";

    }
}
