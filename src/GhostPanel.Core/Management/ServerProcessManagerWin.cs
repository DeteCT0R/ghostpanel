using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Management
{
    // TODO: Come up with way to search for running exe with matching path.  Avoid out of sync PID
    public class ServerProcessManagerWin : IServerProcessManager
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;

        public ServerProcessManagerWin(ILogger<ServerProcessManagerWin> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public void StopServer(GameServer gameServer)
        {
            _logger.LogDebug("Attempting to stop game server");
            if (!IsRunning(gameServer.Pid))
            {
                return;
            }

            try
            {
                Process proc = Process.GetProcessById((int)gameServer.Pid);
                proc.Kill();
                gameServer.Pid = null;
                _repository.Update(gameServer);
                gameServer.Status = ServerStatusStates.Stopped;
                _logger.LogInformation("Killed game server with pid {pid}", gameServer.Pid);
                return;
            }
            catch (ArgumentException e)
            {
                _logger.LogError("Unable to locate PID {pid}", gameServer.Pid);
                return;
            }
        }

        public Process StartServer(GameServer gameServer)
        {
            if (IsRunning(gameServer.Pid))
            {
                _logger.LogDebug("Server {id} is already running with PID {pid}", gameServer.Id, gameServer.Pid);
                return null;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = gameServer.CommandLine;
            start.FileName = Path.Combine(gameServer.HomeDirectory, gameServer.Game.ExeName);
            start.WindowStyle = ProcessWindowStyle.Hidden;
            Process proc = Process.Start(start);
            gameServer.Pid = proc.Id;
            gameServer.Status = ServerStatusStates.Running;
            _repository.Update(gameServer);
            // TODO: Add check to see if process actually started

            return proc;

        }

        public void RestartServer(GameServer gameServer)
        {
            StopServer(gameServer);
            StartServer(gameServer);
        }

        public bool IsRunning(int? pid)
        {
            if (pid == null || pid == 0)
            {
                return false;
            }

            try
            {
                Process proc = Process.GetProcessById((int)pid);
                if (!proc.HasExited)
                {
                    return true;
                }
                return false;

            }
            catch (ArgumentException e)
            {
                _logger.LogDebug("Unable to locate PID {pid}", pid);
                return false;
            }
        }
    }
}
