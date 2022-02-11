using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WWB.IOT.AMQP.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAMQPConsumer(this IApplicationBuilder app, Action<IConsumer> configure)
        {
            IConsumer requiredService = app.ApplicationServices.GetRequiredService<IConsumer>();
            configure(requiredService);
            return app;
        }
    }
}