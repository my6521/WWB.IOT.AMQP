using System;
using System.Threading.Tasks;
using WWB.IOT.AMQP.Options;

namespace WWB.IOT.AMQP
{
    public interface IConsumer : IApplicationMessageReceiver, IDisposable
    {
        bool IsStarted { get; }
        IAMQPOptions Options { get; }
        Task StartAsync(IAMQPOptions options);
        Task StopAsync();
    }
}