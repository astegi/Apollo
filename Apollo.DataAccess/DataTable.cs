using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.DataAccess
{
    /// <summary>
    /// Adatbázis tábla leíró tulajdonság
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class DataTableAttribute : Attribute
    {
        private string tableName;
        private string schemaName;
        private string sequence;

        /// <summary>
        /// A tábla neve
        /// </summary>
        public string TableName
        {
            get { return tableName; }
        }

        /// <summary>
        /// A tábla tulajdonos sémája
        /// </summary>
        public string SchemaName
        {
            get { return schemaName; }
        }

        /// <summary>
        /// A táblához tartozó szekvencia
        /// </summary>
        public string Sequence
        {
            get { return sequence; }
        }

        /// <summary>
        /// Adatbázis tábla leíró tulajdonság
        /// </summary>
        /// <param name="schemaName">A tábla tulajdonos sémája</param>
        /// <param name="tableName">A tábla neve</param>
        public DataTableAttribute(string schemaName, string tableName, string sequence)
        {
            this.schemaName = schemaName;
            this.tableName = tableName;
            this.sequence = sequence;
        }

    }
}
