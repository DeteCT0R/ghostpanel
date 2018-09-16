using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Management.Server
{
    interface IGameConsole
    {
        List<string> GetConsoleOutput();
        string InputToConsole();
    }
}
