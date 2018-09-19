using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Core.Background
{
    public interface IQueuedTask
    {
        void Invoke();
        bool IsDone();
        string GetTaskName();
    }
}
