using System;
using System.Threading.Tasks;

namespace WWB.IOT.AMQP
{
    public class ApplicationMessageReceivedHandlerDelegate : IApplicationMessageReceivedHandler
    {
        private readonly Func<ApplicationMessageReceivedEventArgs, Task> _handler;

        public ApplicationMessageReceivedHandlerDelegate(Func<ApplicationMessageReceivedEventArgs, Task> handler)
        {
            _handler = handler;
        }

        public Task HandleApplicationMessageReceivedAsync(ApplicationMessageReceivedEventArgs context)
        {
            return this._handler(context);
        }
    }
}