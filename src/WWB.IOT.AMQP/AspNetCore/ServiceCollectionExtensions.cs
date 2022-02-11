using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WWB.IOT.AMQP.Options;

namespace WWB.IOT.AMQP.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHostedAMQPConsumerWithServices(this IServiceCollection services,
            Action<AspNetAMQPOptionsBuilder> configure)
        {
            services.AddSingleton<IAMQPOptions>((Func<IServiceProvider, IAMQPOptions>) (s =>
            {
                AspNetAMQPOptionsBuilder serverOptionsBuilder = new AspNetAMQPOptionsBuilder(s);
                configure(serverOptionsBuilder);
                return serverOptionsBuilder.Build();
            }));

            services.AddHostedAMQPConsumer();

            return services;
        }

        private static IServiceCollection AddHostedAMQPConsumer(this IServiceCollection services)
        {
            services.AddSingleton<HostedConsumer>();
            services.AddSingleton<IHostedService>(
                (Func<IServiceProvider, IHostedService>) (s => (IHostedService) s.GetService<HostedConsumer>()));
            services.AddSingleton<IConsumer>((Func<IServiceProvider, IConsumer>) (s => s.GetService<HostedConsumer>()));
            return services;
        }
    }
}