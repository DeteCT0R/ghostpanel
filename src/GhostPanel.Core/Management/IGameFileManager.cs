using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GhostPanel.Core.Managment
{
    public interface IGameFileManager
    {
        Process DownloadGameServerFiles();
        void DeleteGameServerFiles(string dir);
        Process UpdateGameServerFiles();

    }
}
