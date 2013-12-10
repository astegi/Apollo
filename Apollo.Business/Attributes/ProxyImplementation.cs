using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class ProxyImplementationAttribute : Attribute
    {
    }
}
