using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public class BackgroundService
    {
        private readonly List<IQueuedTask> _tasks = new List<IQueuedTask>();

        public BackgroundService()
        {
        }

        public async Task Start()
        {
            var mainLoop = Task.Run(async () =>
            {
                while (true)
                {
                    RunPendingTasks();
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }
            });
        }

        private void RunPendingTasks()
        {
            foreach (var task in _tasks)
            {
                task.Invoke();
            }
        }

        private void removeCompleteTasks()
        {
            foreach (var task in _tasks)
            {
                if (task.IsDone)
                {
                    _tasks.Remove(task);
                }
            }
        }
    }
}
