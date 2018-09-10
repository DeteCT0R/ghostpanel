using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public interface IQueuedTask
    {
        void Invoke();
        bool IsDone { get; }
    }
}
