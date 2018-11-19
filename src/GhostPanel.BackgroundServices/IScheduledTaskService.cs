using System.Collections.Generic;
using System.Threading.Tasks;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.BackgroundServices
{
    public interface IScheduledTaskService
    {
        Task ExecuteScheduledTask(ScheduledTask task);
        ICollection<ScheduledTask> GetScheduledTasks();
        bool IsTimeToRun(ScheduledTask task);
    }
}