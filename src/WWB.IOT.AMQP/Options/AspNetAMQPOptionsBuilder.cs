using System;

namespace WWB.IOT.AMQP.Options
{
    public class AspNetAMQPOptionsBuilder : AMQPOptionsBuilder
    {
        public IServiceProvider ServiceProvider { get; }

        public AspNetAMQPOptionsBuilder(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}