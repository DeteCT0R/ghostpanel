using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Cronos;
using GhostPanel.Core.Util;
using System.Threading.Tasks;
using MediatR;
using GhostPanel.Core.Commands;

namespace GhostPanel.BackgroundServices
{
    public class ScheduledTaskService : IScheduledTaskService
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly IMediator _mediator;

        public ScheduledTaskService(ILogger<ScheduledTaskService> logger, IRepository repository, IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public ICollection<ScheduledTask> GetScheduledTasks()
        {
            return _repository.List<ScheduledTask>();
        }

        /// <summary>
        /// Check the CRON string for the Scheduled Task and determine if it's time to run
        /// </summary>
        /// <param name="task">ScheduledTask object</param>
        /// <returns>Boolean</returns>
        public bool IsTimeToRun(ScheduledTask task)
        {
            string cronString = Util.GetCronString(task);
            CronExpression cronExpression = CronExpression.Parse(cronString);
            DateTime? nextRunTime = cronExpression.GetNextOccurrence(DateTime.UtcNow);
            
            _logger.LogDebug($"Scheduled Task {task.Id}: Current time {DateTime.UtcNow} - Next Run {nextRunTime}");

            if (nextRunTime == null)
            {
                _logger.LogError($"Unable to get next runtime for task ID {task.Id} with Cron String of {cronExpression.ToString()}");
                return false;
            } 

            var delta = (DateTime)nextRunTime - DateTime.UtcNow;
            var lastRunDelta = DateTime.UtcNow - task.LastRuntime;

            return (delta.TotalSeconds < 30 && lastRunDelta.TotalSeconds > 30) ? true : false;
        }

        public async Task ExecuteScheduledTask(ScheduledTask task)
        {
            _logger.LogInformation($"Scheduled Task {task.Id}: Executing");
            switch (task.TaskType)
            {
                case ScheduledTaskType.Message:
                    _logger.LogInformation("MESSAGE TASK: Scheduled message executed");
                    break;
                case ScheduledTaskType.StartServer:
                    _logger.LogInformation($"Task {task.Id} - {task.TaskName}: Starting Server {task.GameServerId}");
                    _mediator.Send(new StartServerCommand(task.GameServerId));
                    break;
                case ScheduledTaskType.StopServer:
                    _logger.LogInformation($"Task {task.Id} - {task.TaskName}: Stop Server {task.GameServerId}");
                    _mediator.Send(new StopServerCommand(task.GameServerId));
                    break;
                case ScheduledTaskType.RestartServer:
                    _logger.LogInformation($"Task {task.Id} - {task.TaskName}: Restart Server {task.GameServerId}");
                    await _mediator.Send(new StopServerCommand(task.GameServerId));
                    _mediator.Send(new StartServerCommand(task.GameServerId));
                    break;
                default:
                    break;
            }

            task.LastRuntime = DateTime.UtcNow;
            _repository.Update(task);
        }
    }
}
