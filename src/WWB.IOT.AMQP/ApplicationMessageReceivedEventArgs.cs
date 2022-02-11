using System;

namespace WWB.IOT.AMQP
{
    public class ApplicationMessageReceivedEventArgs : EventArgs
    {
        public string MessageId { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
    }
}