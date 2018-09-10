using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public interface IBackgroundService
    {
        Task Start();
        void AddTask(IQueuedTask taskToAdd);

    }
}
