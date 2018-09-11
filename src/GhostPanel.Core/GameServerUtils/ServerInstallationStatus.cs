using GhostPanel.Core.Data.Model;
using System.Diagnostics;

namespace GhostPanel.Core.GameServerUtils
{
    public class GameServerStatus
    {
        private Process _serverProcess;
        public ServerInstallStatus status;

        public Process ServerProcess { get => _serverProcess; set => _serverProcess = value; }

        public GameServerStatus()
        {

        }

        public bool IsComplete()
        {
            // TODO - Check exit code
            return _serverProcess.HasExited;
        }
    }
}
