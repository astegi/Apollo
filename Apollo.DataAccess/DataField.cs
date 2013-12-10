using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Apollo.DataAccess
{
    /// <summary>
    /// Egy adott osztály mezőinek jelző attribútuma
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
    public class DataFieldAttribute : Attribute
    {
        private string fieldName;
        private bool isPrimaryKey;
        private bool allowNull;
        private bool required;

        /// <summary>
        /// Kötelező mező?
        /// </summary>
        public bool Required
        {
            get { return required; }
        }

        /// <summary>
        /// Engedélyezett-e a NULL érték?
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
        }

        /// <summary>
        /// Adatbázis mezőnév
        /// </summary>
        public string FieldName
        {
            get { return fieldName; }
        }

        /// <summary>
        /// Elsődleges kulcs?
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
        }

        /// <summary>
        /// Megmondja, hogy a mező adatbázisban mely mező értékét tartalmazza
        /// </summary>
        /// <param name="fieldName">Az adatbázisbeli mező neve</param>
        /// <param name="isPrimaryKey">Az adatbázisbeli mező elsődleges kulcs?</param>
        /// <param name="allowNull">Engedélyezett-e a NULL érték?</param>
        /// <param name="required">Kötelező mező?</param>
        public DataFieldAttribute(string fieldName, bool isPrimaryKey = false, bool allowNull = true, bool required = false)
        {
            this.fieldName = fieldName;
            this.isPrimaryKey = isPrimaryKey;
            this.allowNull = allowNull;
            this.required = required;
        }
    }
}
