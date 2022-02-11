using System;

namespace WWB.IOT.AMQP.Options
{
    public class AMQPOptionsBuilder
    {
        readonly AMQPOptions _options = new AMQPOptions();

        public AMQPOptionsBuilder WithAccessKey(string value)
        {
            _options.AccessKey = value;
            return this;
        }

        public AMQPOptionsBuilder WithAccessSecret(string value)
        {
            _options.AccessSecret = value;
            return this;
        }

        public AMQPOptionsBuilder WithConsumerGroupId(string value)
        {
            _options.ConsumerGroupId = value;
            return this;
        }

        public AMQPOptionsBuilder WithIotInstanceId(string value)
        {
            _options.IotInstanceId = value;
            return this;
        }

        public AMQPOptionsBuilder WithPort(int value)
        {
            _options.Port = value;
            return this;
        }

        public AMQPOptionsBuilder WithHost(string value)
        {
            _options.Host = value;
            return this;
        }

        public AMQPOptionsBuilder WithClientId(string value)
        {
            _options.ClientId = value;
            return this;
        }


        public IAMQPOptions Build()
        {
            return _options;
        }
    }
}