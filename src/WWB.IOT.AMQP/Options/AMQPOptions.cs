namespace WWB.IOT.AMQP.Options
{
    public class AMQPOptions : IAMQPOptions
    {
        public string Host { get; set; }
        public int Port { get;set; }
        public string AccessKey { get; set;}
        public string AccessSecret { get; set;}
        public string ConsumerGroupId { get; set;}
        public string ClientId { get;set; }
        public string IotInstanceId { get;set; }
    }
}