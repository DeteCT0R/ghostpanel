using GhostPanel.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.GameServerUtils
{
    public interface IGameServerManager
    {
        void CreateGameServer(GameServer gameServer);
        void InstallGameServer(GameServer gameServer);
        void RemoveGameServer(GameServer gameServer);
        void DeleteGameServerFiles(GameServer gameServer);
        void ReinstallGameServer(GameServer gameServer);
        void StartServer(GameServer gameServer);
        void StopServer(GameServer gameServer);
        void RestartServer(GameServer gameServer);

    }
}
