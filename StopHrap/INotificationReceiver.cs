using System.Threading.Tasks;

namespace StopHrap
{
    public interface INotificationReceiver
    {
        Task StartAsync();
        void Stop();
    }
}