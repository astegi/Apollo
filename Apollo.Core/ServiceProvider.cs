using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Apollo.Core.Extensions;
using Apollo.Core.Attributes;
using Apollo.Core.Managers;
using Apollo.Core.Miscellaneous;
using Apollo.DataAccess;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Apollo.Core
{
    /// <summary>
    /// Business Logic proxy segédosztálya
    /// </summary>
    public class ServiceProvider
    {
        private static ServiceProvider instance;
        
        /// <summary>
        /// Business Logic proxy segédosztály
        /// </summary>
        internal static ServiceProvider Instance
        {
            get { return ServiceProvider.instance ?? (ServiceProvider.instance = new ServiceProvider()); }
        }


        private const string BUSINESS_ASSEMBLY = "Apollo.Business.dll";
        private const string BUSINESS_ENTITIES_ASSEMBLY = "Apollo.Business.Entities.dll";
        public static Assembly BllAssembly { get; private set; }
        public static Assembly BllEntitiesAssembly { get; private set; }

        private ServiceProvider()
        {
        }

        /// <summary>
        /// Feltölti a Proxy osztályt a hozzá tartozó üzleti logika osztályokkal
        /// </summary>
        public void FillProxy()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            BllAssembly = Assembly.LoadFrom(Path.Combine(path, BUSINESS_ASSEMBLY));
            BllEntitiesAssembly = Assembly.LoadFrom(Path.Combine(path, BUSINESS_ENTITIES_ASSEMBLY));

            Type proxyType = typeof(Proxy);
            Type proxyContractAttributeType = typeof(ProxyContractAttribute);

            FieldInfo[] fields = proxyType.GetFields(BindingFlags.Public | BindingFlags.Static);
            fields = fields.Where(x => x.FieldType.GetCustomAttributes(proxyContractAttributeType,false).Length > 0).ToArray();

            foreach (FieldInfo fi in fields)
            {
                Type[] implementations = BllAssembly.GetInterfaceImplementationTypes(fi.FieldType);

                if (implementations.Length == 1)
                {
                    object bllInstance = Activator.CreateInstance(implementations[0]);
                    fi.SetValue(null, bllInstance);
                }
            }
        }

        /// <summary>
        /// Feltölti a StatusManager osztályt a halmazokkal
        /// </summary>
        public void FillEnums()
        {
            List<Enumeration> dbEnumerations = DataConnector.ListDataEntities<Enumeration>(Query.Create("SELECT * FROM Enumeration"));

            Type[] types = BllEntitiesAssembly.GetTypes();
            List<Enumeration> enumerations = new List<Enumeration>();
            foreach (Type type in types)
            {
                if (!type.IsEnum || type.GetCustomAttributes(typeof(DatabaseMappedEnumAttribute), false).Length != 1)
                    continue;

                FieldInfo[] fields = type.GetFields();
                enumerations.Clear();
                foreach (FieldInfo field in fields)
                {
                    object[] attributes = field.GetCustomAttributes(typeof(DatabaseMappedEnumDescriptorAttribute), false);
                    if (attributes.Length != 1)
                        continue;

                    DatabaseMappedEnumDescriptorAttribute descriptorAttribute = (DatabaseMappedEnumDescriptorAttribute)attributes[0];
                    Enumeration enumeration = new Enumeration { EnumCategory = descriptorAttribute.Category, EnumName = descriptorAttribute.Name, EnumValue = enumerations.Count };
                    enumerations.Add(enumeration);
                }


                if (enumerations.Count > 0)
                {
                    List<Enumeration> dbEnumeration = dbEnumerations.FindAll(x => x.EnumCategory == enumerations[0].EnumCategory);
                    EnumerationManager.AddToStore(type, enumerations.Count);
                    EnumerationManager.AddEnumsToStore(type, dbEnumeration);
                }
            }
        }

        public void FillProxyByProxyClass()
        {
            Type proxyType = typeof(Proxy);

            RemotingHelper.CertificateValidation += new RemoteCertificateValidationCallback(RemotingHelper_CertificateValidation);
            RemotingHelper.InitProxy(null, null, null, proxyType);
        }

        bool RemotingHelper_CertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static bool WaitInitializationEnd()
        {
            return RemotingHelper.InitializationDone.WaitOne(20*Constants.TIMEOUT);
        }
    }
}
