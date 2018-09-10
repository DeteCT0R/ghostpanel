using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    interface IBackgroundService
    {
        Task Start();
        void AddTask(IQueuedTask taskToAdd);
        void RunPendingTasks();
        void removeCompleteTasks();
    }
}
