using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Exceptions
{
    public class FailedToLocateGameFileManager : Exception
    {
        public FailedToLocateGameFileManager()
        {
        }

        public FailedToLocateGameFileManager(string message)
            : base(message)
        {
        }

        public FailedToLocateGameFileManager(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
