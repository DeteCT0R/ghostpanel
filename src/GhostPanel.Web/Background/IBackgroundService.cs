using GhostPanel.Core.Managment;
using System.Threading.Tasks;

namespace GhostPanel.Web.Background
{
    public interface IBackgroundService
    {
        Task Start();
        void AddTask(IQueuedTask taskToAdd);

    }
}
