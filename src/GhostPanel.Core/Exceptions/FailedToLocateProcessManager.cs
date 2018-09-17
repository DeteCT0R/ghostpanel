using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Exceptions
{

    public class FailedToLocateProcessManager : Exception
    {
        public FailedToLocateProcessManager()
        {
        }

        public FailedToLocateProcessManager(string message)
            : base(message)
        {
        }

        public FailedToLocateProcessManager(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
