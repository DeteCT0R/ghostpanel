using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GhostPanel.Core.Managment
{
    public interface IGameFileManager
    {
        void DownloadGameServerFiles();
        void DeleteGameServerFiles(string dir);
        void UpdateGameServerFiles();

    }
}
