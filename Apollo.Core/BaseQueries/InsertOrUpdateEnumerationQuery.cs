using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.DataAccess;
using Apollo.Core.Miscellaneous;

namespace Apollo.Core.BaseQueries
{
    class InsertOrUpdateEnumerationQuery : Query
    {
        private Enumeration enumeration;

        public override string CommandText
        {
            get { return "InsertOrUpdateEnumeration"; }
        }

        public override System.Data.CommandType CommandType
        {
            get { return System.Data.CommandType.StoredProcedure; }
        }


        public InsertOrUpdateEnumerationQuery(Enumeration enumeration)
        {
            this.enumeration = enumeration;
        }

        protected override void AddParameters()
        {
            AddInParameter("enumName", enumeration.EnumName);
            AddInParameter("enumCategory", enumeration.EnumCategory);
            AddInParameter("enumValue", enumeration.EnumValue);
        }
    }
}
