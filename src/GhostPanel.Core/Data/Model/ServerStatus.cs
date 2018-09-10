using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public enum ServerStatus
    {
        Running,
        Updating,
        Stopped,
        Error,
        Installing,
        Unknown
    }
}
