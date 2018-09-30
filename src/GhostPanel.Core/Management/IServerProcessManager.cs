using System.Diagnostics;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Management
{
    public interface IServerProcessManager
    {
        Process RestartServer(GameServer gameServer);
        Process StartServer(GameServer gameServer);
        void StopServer(GameServer gameServer);
        bool IsRunning(int? pid);
        GameServer HandleCrashedServer(GameServer gameServer);
    }
}