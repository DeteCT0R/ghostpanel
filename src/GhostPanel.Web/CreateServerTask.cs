using GhostPanel.Core.GameServerUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public class CreateServerTask : IQueuedTask
    {
        public bool IsDone;
        public int Pid;
        private GameServerManager _gameServerManager;

        public CreateServerTask(GameServerManager gameServerManager)
        {
            IsDone = false;
            _gameServerManager = gameServerManager;
        }

        public void Invoke()
        {
            Console.WriteLine("Running new task");
            _gameServerManager.InstallGameServer();
            IsDone = true;
        }

        bool IQueuedTask.IsDone()
        {
            return IsDone;
        }
    }
}
