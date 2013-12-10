using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apollo.Core.Miscellaneous;
using Apollo.DataAccess;
using Apollo.Test.DataAccess;

namespace Apollo.Test
{
    [TestClass]
    public class DataAccessTests
    {
        [TestMethod]
        public void TestListEntities()
        {
            List<Enumeration> enumerations = DataConnector.ListDataEntities<Enumeration>(new EnumerationQuery());
            
            Assert.IsTrue(enumerations.Count > 0);
        }
    }
}
