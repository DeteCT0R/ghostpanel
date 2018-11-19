using GhostPanel.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Util
{
    public static class Util
    {
        public static string GetCronString(ScheduledTask scheduled)
        {
            return string.Format("{0} {1} {2} {3} {4}", scheduled.Minute, scheduled.Hour, scheduled.DayOfMonth, scheduled.Month, scheduled.DayOfWeek);
        }
    }
}
