using System;

namespace Apollo.DataAccess.Entities
{
    [Serializable]
    [DataTable("dbo", "MessageType", "")]
    public class TraceEventData : DataCore
    {
        [DataField("MessageCode")]
        private int messageCode;
        /// <summary>
        /// Esemény azonosító 
        /// </summary>
        public int MessageCode
        {
            get { return messageCode; }
            set { messageCode = value; }
        }

        [DataField("Severity")]
        private string severity;
        /// <summary>
        /// Esemény típus
        /// </summary>
        public string Severity
        {
            get { return severity; }
            set { severity = value; }
        }

        [DataField("Language")]
        private string language;
        /// <summary>
        /// Nyelv
        /// </summary>
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        [DataField("Facility")]
        private string facility;
        /// <summary>
        /// Forrás megkötés
        /// </summary>
        public string Facility
        {
            get { return facility; }
            set { facility = value; }
        }

        [DataField("SymbolicName")]
        private string symbolicName;
        /// <summary>
        /// Esemény név azonosító
        /// </summary>
        public string SymbolicName
        {
            get { return symbolicName; }
            set { symbolicName = value; }
        }

        [DataField("Description")]
        private string description;
        /// <summary>
        /// Esemény leírás
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
