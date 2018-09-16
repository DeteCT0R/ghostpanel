using GhostPanel.Core.GameServerUtils;
using System;
using GhostPanel.Core.Managment.GameFiles;
using Microsoft.Extensions.Logging;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Background
{
    public class GenericBackgroundTask : IQueuedTask
    {
        public bool IsDone;
        private readonly ILogger _logger;
        private readonly Action _action;
        private readonly string _name;

        public GenericBackgroundTask(Action action, ILoggerFactory logger, string name = "Generic Task")
        {
            IsDone = false;
            _action = action;
            _name = name;
            _logger = logger.CreateLogger<GenericBackgroundTask>();

        }

        public void Invoke()
        {
            _logger.LogInformation("Invoking task {name}", _name);
            _action.Invoke();
            IsDone = true;
        }

        bool IQueuedTask.IsDone()
        {
            return IsDone;
        }

        public string GetTaskName()
        {
            return _name;
        }
    }
}
