namespace WWB.IOT.AMQP
{
    public interface IApplicationMessageReceiver
    {
        IApplicationMessageReceivedHandler ApplicationMessageReceivedHandler { get; set; }
    }
}