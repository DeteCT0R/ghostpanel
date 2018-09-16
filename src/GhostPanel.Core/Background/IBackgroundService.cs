using GhostPanel.Core.Managment;
using System.Threading.Tasks;

namespace GhostPanel.Core.Background
{
    public interface IBackgroundService
    {
        Task Start();
        void AddTask(IQueuedTask taskToAdd);

    }
}
