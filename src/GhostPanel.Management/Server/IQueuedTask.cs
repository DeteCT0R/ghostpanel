using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Management.Server
{
    public interface IQueuedTask
    {
        void Invoke();
        bool IsDone();
    }
}
