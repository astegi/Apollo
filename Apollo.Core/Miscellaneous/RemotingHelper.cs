using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.ServiceModel;
using System.Collections;
using System.Runtime.Remoting.Channels.Http;
using System.Reflection;
using Apollo.Core.Managers;
using Apollo.Core.Attributes;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Runtime.Remoting;
using System.Threading;

namespace Apollo.Core.Miscellaneous
{
    public delegate IChannel ChannelFactory(Hashtable props, IClientFormatterSinkProvider formatter);

    public class RemotingHelper
    {
        public static Hashtable ClientChannelSinkProperties { get; private set; }

        private static List<IChannel> unregisteredChannels;
        public static event RemoteCertificateValidationCallback CertificateValidation;

        public static ManualResetEvent InitializationDone = new ManualResetEvent(false);

        public static void InitProxy(string username, string password, string domain, Type proxyType)
        {
            FieldInfo[] fis = proxyType.GetFields();
            Type attributeType = typeof(ProxyContractAttribute);

            ServicePointManager.ServerCertificateValidationCallback = CertificateValidation;

            BinaryClientFormatterSinkProvider formatterProvider = new BinaryClientFormatterSinkProvider();

            CredentialCache credCache = InitChannel(username, password, domain, formatterProvider);

            foreach (FieldInfo fi in fis)
            {
                if (fi.FieldType.GetCustomAttributes(attributeType, false).Length > 0)
                {
                    object proxy = Creation(fi.FieldType);
                    if (proxy != null && proxy is MarshalByRefObject)
                    {
                        IDictionary props = ChannelServices.GetChannelSinkProperties(proxy);
                        if (props != null)
                        {
                            props["credentials"] = credCache;
                            props["preauthenticate"] = credCache != null;
                        }
                    }
                    fi.SetValue(null, proxy);
                }
            }

            RestoreChannels(unregisteredChannels);
        }

        private static CredentialCache InitChannel(string username, string password, string domain, IClientFormatterSinkProvider formatterProvider)
        {
            Hashtable props = new Hashtable();
            CredentialCache credCache = null;
            if (username != null && password != null)
            {
                NetworkCredential cred = new NetworkCredential(username, password, domain);
                credCache = new CredentialCache();
                credCache.Add(Properties.Settings.Default.ServiceUrl, "NTLM", cred);
                props["credentials"] = credCache;
                props["preauthenticate"] = true;
                props["useDefaultCredentials"] = false;
            }
            else
                props["useDefaultCredentials"] = true;

            ClientChannelSinkProperties = props;

            unregisteredChannels = new List<IChannel>();
            foreach (IChannel ch in ChannelServices.RegisteredChannels)
            {
                unregisteredChannels.Add(ch);
                ChannelServices.UnregisterChannel(ch);
            }
            ChannelServices.RegisterChannel(new HttpClientChannel(props, formatterProvider), false);
            return credCache;
        }

        private static void RestoreChannels(List<IChannel> channelList)
        {
            if (channelList != null && channelList.Count > 0)
            {
                foreach (IChannel ch in ChannelServices.RegisteredChannels)
                    ChannelServices.UnregisterChannel(ch);
                foreach (IChannel channel in channelList)
                    ChannelServices.RegisterChannel(channel, false);
            }
        }
        private static object Creation(Type type)
        {
            string url = String.Concat(Properties.Settings.Default.ServiceUrl, type.Name.Substring(1), ".rem");
            return Properties.Settings.Default.UseCustomProxy ? new ProxyClass(type, url).GetTransparentProxy() : Activator.GetObject(type, url);
        }


        public static void InitializeServices<TClientServiceType, TServerServiceType>()
            where TClientServiceType : Attribute
            where TServerServiceType : Attribute
        {
            InitializationDone.Reset();
            Dictionary<Type, bool> interfaces = new Dictionary<Type, bool>();
            Type clientServiceType = typeof(TClientServiceType);
            Type serverServiceType = typeof(TServerServiceType);
            Type[] allClientServiceTypes = clientServiceType.Assembly.GetTypes();
            Type[] allServerServiceTypes = serverServiceType.Assembly.GetTypes();

            foreach (Type type in allClientServiceTypes)
            {
                if (type.GetCustomAttributes(clientServiceType, false).Length > 0)
                    interfaces.Add(type, true);
            }

            foreach (Type type in allServerServiceTypes)
            {
                Type[] interfaceTypes = type.GetInterfaces().Where(interfaces.ContainsKey).ToArray();
                foreach (Type interfaceType in interfaceTypes)
                {
                    if (type.GetConstructor(new Type[0]) != null)
                    {
                        WellKnownServiceTypeEntry entry = new WellKnownServiceTypeEntry(type,
                            String.Concat(interfaceType.Name.Substring(1), ".rem"), WellKnownObjectMode.Singleton);
                        RemotingConfiguration.RegisterWellKnownServiceType(entry);
                        interfaces.Remove(interfaceType);
                        break;
                    }
                }
            }

            InitializationDone.Set();
        }
    }
}
