using System;
using System.Threading.Tasks;

namespace WWB.IOT.AMQP
{
    public static class ConsumerExtensions
    {
        public static IConsumer UseApplicationMessageReceivedHandler(this IConsumer client, Func<ApplicationMessageReceivedEventArgs, Task> handler)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            if (handler == null)
            {
                return client.UseApplicationMessageReceivedHandler((IApplicationMessageReceivedHandler)null);
            }

            return client.UseApplicationMessageReceivedHandler(new ApplicationMessageReceivedHandlerDelegate(handler));
        
        }
        
        public static IConsumer UseApplicationMessageReceivedHandler(this IConsumer client, IApplicationMessageReceivedHandler handler)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
    
            client.ApplicationMessageReceivedHandler = handler;
            
            return client;
        }
    }
}