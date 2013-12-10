using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple=false)]
    public class DatabaseMappedEnumAttribute : Attribute
    {
    }
}
