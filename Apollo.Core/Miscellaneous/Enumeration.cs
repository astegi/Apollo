using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.DataAccess;

namespace Apollo.Core.Miscellaneous
{
    /// <summary>
    /// Halmaz típus
    /// </summary>
    [DataTable("dbo", "Enumerations", "")]
    public class Enumeration : DataCore
    {
        [DataField("enumName", false, false, true)]
        string enumName;
        
        [DataField("storedValue", false, false, true)]
        int storedValue;

        [DataField("enumValue", false, false, true)]
        int enumValue;

        [DataField("enumCategory", false, false, true)]
        string enumCategory;

        /// <summary>
        /// Halmazelem kategória
        /// </summary>
        public string EnumCategory
        {
            get { return enumCategory; }
            set { enumCategory = value; }
        }

        /// <summary>
        /// Halmazelem neve
        /// </summary>
        public string EnumName
        {
            get { return enumName; }
            set { enumName = value; }
        }

        /// <summary>
        /// Halmazelem tárolt értéke
        /// </summary>
        public int StoredValue
        {
            get { return storedValue; }
            set { storedValue = value; }
        }

        /// <summary>
        /// Halmazelem sorosított értéke
        /// </summary>
        public int EnumValue
        {
            get { return enumValue; }
            set { enumValue = value; }
        }
    }
}
