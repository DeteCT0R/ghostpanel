using GhostPanel.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.GameServerUtils
{
    public interface IGameServerManager
    {
        void InstallGameServer(GameServer gameServer);
        void DeleteGameServer(GameServer gameServer);
        void ReinstallGameServer(GameServer gameServer);
        int? GetGameServerId();
        void StartServer();
        void StopServer();

    }
}
