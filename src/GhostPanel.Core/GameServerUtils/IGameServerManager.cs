﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.GameServerUtils
{
    public interface IGameServerManager
    {
        void InstallGameServer();
        void DeleteGameServer();
        void ReinstallGameServer();
        bool IsRunning();
        void StartServer();
        void StopServer();
    }
}
