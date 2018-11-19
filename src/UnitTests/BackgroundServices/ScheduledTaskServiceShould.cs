using GhostPanel.BackgroundServices;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.BackgroundServices
{
    public class ScheduledTaskServiceShould
    {
        [Fact]
        public void IsTimeToRun_InFuture_ReturnFalse()
        {
            var logger = Mock.Of<ILogger<ScheduledTaskService>>();
            var repo = Mock.Of<IRepository>();
            var scheduledTaskService = new ScheduledTaskService(logger, repo);
            var task = new ScheduledTask();
            task.Minute = "*/3";
            //var result = scheduledTaskService.IsTimeToRun(task);
            Console.WriteLine("");
        }
    }
}
