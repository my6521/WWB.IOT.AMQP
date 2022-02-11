using System.Threading.Tasks;

namespace WWB.IOT.AMQP
{
    public interface IApplicationMessageReceivedHandler
    {
        Task HandleApplicationMessageReceivedAsync(ApplicationMessageReceivedEventArgs eventArgs);
    }
}