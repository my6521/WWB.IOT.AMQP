using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amqp;
using Amqp.Framing;
using Amqp.Sasl;
using WWB.IOT.AMQP.Options;

namespace WWB.IOT.AMQP
{
    public class Consumer : IConsumer
    {
        private int _count = 0;
        private int _intervalTime = 10000;
        private Address _address;

        public bool IsStarted { get; set; }
        public IAMQPOptions Options { get; private set; }
        public IApplicationMessageReceivedHandler ApplicationMessageReceivedHandler { get; set; }

        public Task StartAsync(IAMQPOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));

            var sign = Sign.Create(options.AccessKey, options.AccessSecret, options.ClientId, options.IotInstanceId,
                options.ConsumerGroupId);

            DoConnectAmqp(sign.UserName, sign.Password);

            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }

        private void DoConnectAmqp(string userName, string password)
        {
            _address = new Address(Options.Host, Options.Port, userName, password);
            //创建Connection。
            ConnectionFactory cf = new ConnectionFactory();
            //如果需要，使用本地TLS。
            //cf.SSL.ClientCertificates.Add(GetCert());
            //cf.SSL.RemoteCertificateValidationCallback = ValidateServerCertificate;
            cf.SASL.Profile = SaslProfile.External;
            cf.AMQP.IdleTimeout = 120000;
            //cf.AMQP.ContainerId、cf.AMQP.HostName请自定义。
            cf.AMQP.ContainerId = "client.1.2";
            cf.AMQP.HostName = "contoso.com";
            cf.AMQP.MaxFrameSize = 8 * 1024;
            var connection = cf.CreateAsync(_address).Result;

            this.IsStarted = true;

            //Connection Exception已关闭。
            connection.AddClosedCallback(ConnClosed);

            //接收消息。
            DoReceive(connection);
        }

        private void DoReceive(Connection connection)
        {
            //创建Session。
            var session = new Session(connection);

            //创建Receiver Link并接收消息。
            var receiver = new ReceiverLink(session, "queueName", null);

            receiver.Start(20, (link, message) =>
            {
                object messageId = message.ApplicationProperties["messageId"];
                object topic = message.ApplicationProperties["topic"];
                string body = Encoding.UTF8.GetString((Byte[]) message.Body);
                //注意：此处不要有耗时的逻辑，如果这里要进行业务处理，请另开线程，否则会堵塞消费。如果消费一直延时，会增加消息重发的概率。
                Console.WriteLine("receive message, topic=" + topic + ", messageId=" + messageId + ", body=" + body);
                this.ApplicationMessageReceivedHandler?.HandleApplicationMessageReceivedAsync(
                    new ApplicationMessageReceivedEventArgs
                    {
                        MessageId = messageId.ToString(),
                        Topic = topic.ToString(),
                        Body = body
                    });

                //ACK消息。
                link.Accept(message);
            });
        }

        //连接发生异常后，进入重连模式。
        //这里只是一个简单重试的示例，您可以采用指数退避方式，来完善异常场景，重连策略。
        private void ConnClosed(IAmqpObject _, Error e)
        {
            this.IsStarted = false;
            Console.WriteLine("ocurr error: " + e);
            if (_count < 3)
            {
                _count += 1;
                Thread.Sleep(_intervalTime * _count);
            }
            else
            {
                Thread.Sleep(120000);
            }

            //重连。
            DoConnectAmqp(_address.User, _address.Password);
        }
    }
}