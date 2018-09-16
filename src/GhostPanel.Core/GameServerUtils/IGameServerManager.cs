using GhostPanel.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.GameServerUtils
{
    public interface IGameServerManager
    {
        void InstallGameServer();
        void DeleteGameServer();
        void ReinstallGameServer();
        int? GetGameServerId();
        bool IsRunning();
        void StartServer();
        void StopServer();
        void SetGameServer(GameServer gameServer);
        void CreateGameServer(GameServer gameServer);
    }
}
