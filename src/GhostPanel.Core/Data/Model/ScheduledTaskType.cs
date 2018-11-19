using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public enum ScheduledTaskType
    {
        Message,
        Command,
        StopServer,
        StartServer,
        RestartServer
    }
}
