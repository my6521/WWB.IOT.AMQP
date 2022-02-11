using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WWB.IOT.AMQP.Options;

namespace WWB.IOT.AMQP.AspNetCore
{
    public class HostedConsumer : Consumer, IHostedService
    {
        private readonly IAMQPOptions _options;

        public HostedConsumer(IAMQPOptions options)
        {
            this._options = options ?? throw new ArgumentNullException(nameof (options));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            base.StartAsync(this._options);
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => base.StopAsync();
    }
}