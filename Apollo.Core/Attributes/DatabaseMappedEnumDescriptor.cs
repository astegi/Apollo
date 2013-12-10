using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple=false)]
    public class DatabaseMappedEnumDescriptorAttribute : Attribute
    {
        private string name;
        private string category;

        public string Name
        {
            get { return name; }
        }
        public string Category
        {
            get { return category; }
        }


        public DatabaseMappedEnumDescriptorAttribute(string name, string category)
        {
            this.name = name;
            this.category = category;
        }
    }
}
