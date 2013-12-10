using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple=false)]
    public class ProxyContractAttribute:Attribute
    {
    }
}
