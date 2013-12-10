using Apollo.DataAccess;
using Apollo.DataAccess.Entities;
using System;

namespace Apollo.Core.Entities
{
    [Serializable]
    public class TraceEvent : Convertable<TraceEventData>
    {
        /// <summary>
        /// Esemény azonosító 
        /// </summary>
        public int MessageCode
        {
            get;
            set;
        }

        /// <summary>
        /// Esemény típus
        /// </summary>
        public string Severity
        {
            get;
            set;
        }

        /// <summary>
        /// Nyelv
        /// </summary>
        public string Language
        {
            get;
            set;
        }

        /// <summary>
        /// Forrás megkötés
        /// </summary>
        public string Facility
        {
            get;
            set;
        }

        /// <summary>
        /// Esemény név azonosító
        /// </summary>
        public string SymbolicName
        {
            get;
            set;
        }

        /// <summary>
        /// Esemény leírás
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public TraceEvent(TraceEventData data)
            : base(data)
        {
        }
    }
}
