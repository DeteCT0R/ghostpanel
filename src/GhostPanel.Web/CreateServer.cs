using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public class TestTask : IQueuedTask
    {
        public bool IsDone => throw new NotImplementedException();

        public void Invoke()
        {
            Console.WriteLine("Running new task");
        }
    }
}
