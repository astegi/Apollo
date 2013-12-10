using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.DataAccess;

namespace Apollo.Test.DataAccess
{
    class EnumerationQuery : Query
    {
        public override string CommandText
        {
            get { return "SELECT * FROM [dbo].[Enumeration]"; }
        }
    }
}
