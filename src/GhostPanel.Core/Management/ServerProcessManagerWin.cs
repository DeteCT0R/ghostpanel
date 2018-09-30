using System;
using System.Diagnostics;
using System.IO;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management.Server;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Management
{
    // TODO: Come up with way to search for running exe with matching path.  Avoid out of sync PID
    public class ServerProcessManagerWin : IServerProcessManager
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly ICommandlineProcessor _commandlineProcessor;

        public ServerProcessManagerWin(ILoggerFactory logger, IRepository repository, ICommandlineProcessor commandlineProcessor)
        {
            _logger = logger.CreateLogger<ServerProcessManagerWin>();
            _repository = repository;
            _commandlineProcessor = commandlineProcessor;
        }

        public void StopServer(GameServer gameServer)
        {
            _logger.LogDebug("Attempting to stop game server");
            if (!IsRunning(gameServer.GameServerCurrentStats.Pid))
            {
                return;
            }

            try
            {
                Process proc = Process.GetProcessById((int)gameServer.GameServerCurrentStats.Pid);
                proc.Kill();
                _logger.LogInformation("Killed game server with pid {pid}", gameServer.GameServerCurrentStats.Pid);
                return;
            }
            catch (ArgumentException e)
            {
                _logger.LogError("Unable to locate PID {pid}", gameServer.GameServerCurrentStats.Pid);
                return;
            } 
        }

        public Process StartServer(GameServer gameServer)
        {
            if (!gameServer.IsEnabled)
            {
                _logger.LogError("Game server {id} is not enabled.  Cannot start", gameServer.Id);
                return null;
            }
            if (IsRunning(gameServer.GameServerCurrentStats.Pid))
            {
                _logger.LogDebug("Server {id} is already running with PID {pid}", gameServer.Id, gameServer.GameServerCurrentStats.Pid);
                return null;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = _commandlineProcessor.InterpolateCommandline(gameServer);
            if (gameServer.CustomCommandLineArgs != null)
            {
                start.Arguments = start.Arguments + " " +
                                  _commandlineProcessor.InterpolateCustomCommandline(gameServer.CustomCommandLineArgs);
            }
            start.FileName = Path.Combine(gameServer.HomeDirectory, gameServer.Game.ExeName);
            start.WindowStyle = ProcessWindowStyle.Hidden;
            Process proc = Process.Start(start);
            // TODO: Add check to see if process actually started

            return proc;

        }

        public Process RestartServer(GameServer gameServer)
        {
            StopServer(gameServer);
            return StartServer(gameServer);
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

        /// <summary>
        /// Handle a server that has a PID set but has no executable running.
        /// Attempt to restart 3 times.  If it continues to fail stop the server
        /// </summary>
        /// <param name="gameServer"></param>
        public GameServer HandleCrashedServer(GameServer gameServer)
        {
            // TODO : Remove this method
            if (gameServer.GameServerCurrentStats.Status != ServerStatusStates.Crashed)
            {
                _logger.LogDebug("Game server {id} has a PID set but is not running.  Marking as crashed", gameServer.Id);
                gameServer.GameServerCurrentStats.Status = ServerStatusStates.Crashed;
                _repository.Update(gameServer);

            }

            if (gameServer.GameServerCurrentStats.RestartAttempts < 3) // TODO: Move max restarts to config
            {
                gameServer.GameServerCurrentStats.RestartAttempts++;
                _logger.LogDebug("Attempt #{attempt} to restart server {id}", gameServer.GameServerCurrentStats.RestartAttempts, gameServer.Id);
                StartServer(gameServer);
                _repository.Update(gameServer);
            }
            else
            {
                _logger.LogDebug("Server {id} has hit the max restart attempts.  Stopping server", gameServer.Id);
                gameServer.GameServerCurrentStats.Status = ServerStatusStates.Stopped;
                gameServer.GameServerCurrentStats.Pid = null;
                StopServer(gameServer);
                _repository.Update(gameServer);
            }

            return gameServer;
        }
    }
}
