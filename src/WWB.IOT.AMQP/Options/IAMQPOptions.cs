namespace WWB.IOT.AMQP.Options
{
    public interface IAMQPOptions
    {
        public string Host { get; }
        public int Port { get;  }
        public string AccessKey { get; }
        public string AccessSecret { get; }
        public string ConsumerGroupId { get; }
        public string ClientId { get; }
        public string IotInstanceId { get; }
    }
}