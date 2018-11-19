using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public class ScheduledTask : DataEntity
    {
        public string TaskName { get; set; }
        public ScheduledTaskType TaskType { get; set; }
        public int GameServerId { get; set; }
        public string Second { get; set; } = "*";
        public string Minute { get; set; } = "*";
        public string Hour { get; set; } = "*";
        public string DayOfMonth { get; set; } = "*";
        public string Month { get; set; } = "*";
        public string DayOfWeek { get; set; } = "*";
        public DateTime LastRuntime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User CreatedBy { get; set; }
        public User ModifiedBy { get; set; }
        public GameServer GameServer { get; set; }
    }
}
