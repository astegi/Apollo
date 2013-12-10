using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Windows.Interop;

namespace Apollo.Core.Miscellaneous
{
    class ProxyClass : RealProxy
    {
        private Type type;
        private string url;
        private string uri;

        IMessageSink sinkChain;
        public IWin32Window Owner { get; set; }



        public ProxyClass(Type type, string url)
            :base(type)
        {
            this.type = type;
            this.url = url;
            Initialize();
        }

        private void Initialize()
        {
            IChannel[] registeredChannels = ChannelServices.RegisteredChannels;
            foreach (IChannel channel in registeredChannels)
            {
                if (channel is IChannelSender)
                {
                    IChannelSender channelSender = (IChannelSender)channel;
                    sinkChain = channelSender.CreateMessageSink(url, null, out uri);

                    if (sinkChain != null)
                        break;
                }
            }
            if (sinkChain == null)
                throw new Exception("No channel has been found for " + url);

            IDictionary propSink = GetChannelSinkProperties(sinkChain);
            foreach (DictionaryEntry e in RemotingHelper.ClientChannelSinkProperties)
                propSink[e.Key] = e.Value;
        }

        private static IDictionary GetChannelSinkProperties(IMessageSink sink1)
        {
            IClientChannelSink sink2 = sink1 as IClientChannelSink;
            if (sink2 == null)
                throw new ApplicationException("IClientChannelSinknek kell lennie");
            ArrayList list1 = new ArrayList();
            while (true)
            {
                IDictionary dictionary1 = sink2.Properties;
                if (dictionary1 != null)
                    list1.Add(dictionary1);
                sink2 = sink2.NextChannelSink;
                if (sink2 == null)
                    return new AggregateDictionary(list1);
            }
        }



        public override IMessage Invoke(IMessage msg)
        {
            msg.Properties["__Uri"] = url;

            SyncProcessMessageDelegate syncDelegate = sinkChain.SyncProcessMessage;
            
            IMessage retMsg = null;

            if (Owner == null)
            {
                retMsg = sinkChain.SyncProcessMessage(msg);
            }
            else
            {
                IAsyncResult result = syncDelegate.BeginInvoke(msg, null, null);
                result.AsyncWaitHandle.WaitOne(Constants.TIMEOUT);

                if (!result.IsCompleted)
                    throw new TimeoutException();
                retMsg = syncDelegate.EndInvoke(result);

                result.AsyncWaitHandle.Close();
            }
            return retMsg;
        }

        
        private delegate IMessage SyncProcessMessageDelegate(IMessage msg);
    }
}
