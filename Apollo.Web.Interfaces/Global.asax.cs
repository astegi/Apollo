using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using Apollo.Business.Attributes;
using Apollo.Core.Attributes;
using Apollo.Core.Cache;
using Apollo.Core.Miscellaneous;

namespace Apollo.Web.Interfaces
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Hashtable props = new Hashtable();
            props["name"] = "MyChannel";
            props["priority"] = "100";
            HttpChannel channel = new HttpChannel(props, new BinaryClientFormatterSinkProvider(), new BinaryServerFormatterSinkProvider());
            ChannelServices.RegisterChannel(channel, false);

            RemotingHelper.InitializeServices<ProxyContractAttribute, ProxyImplementationAttribute>();
            channel.StartListening(null);

            CacheManager.Initialize();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Tracer.LogException(sender as Exception);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}