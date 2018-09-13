using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Managment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;

namespace GhostPanel.Core.GameServerUtils
{
    public class GameServerManager : IGameServerManager
    {

        private GameServer _gameServer;
        private readonly IGameFileManager _gameFileManager;
        private readonly IRepository _repository;
        public GameServerStatus gameServerStatus;
        private readonly ILogger _logger;
        private readonly SteamCmd _steamCmd;

        public GameServerManager(IRepository repository, SteamCmd steamCmd, ILogger<GameServerManager> logger)
        {
            _repository = repository;
            _logger = logger;
            _steamCmd = steamCmd;
            gameServerStatus = new GameServerStatus() { status = ServerStatusStates.Unknown };
        }


        private void InitGameFileManager()
        {
            if (_gameServer.Game.SteamAppId)
            {
                _gameFileManager = new SteamCmdGameFiles(_gameServer.HomeDirectory, _gameServer.Game.SteamAppId, _steamCmd, _logger);
            }
        }

        /// <summary>
        /// Allows the game server to be set after creation.  This lets the GameServerManager be created via dependancy injection
        /// </summary>
        /// <param name="gameServer"></param>
        public void SetGameServer(GameServer gameServer)
        {
            _gameServer = gameServer;
        }

        public void DeleteGameServer()
        {
            try
            {
                Directory.Delete(_gameServer.HomeDirectory, true);
            } catch (Exception e)
            {
                _logger.LogError("Failed to delete game server {id}", _gameServer.Id);
            }
        }

        public void InstallGameServer()
        {
            // TODO - Check if there's an active install 
            Process proc = _gameFileManager.downloadGame(_gameServer.HomeDirectory, _gameServer.Game.SteamAppId);
            gameServerStatus.ServerProcess = proc;
            gameServerStatus.status = ServerStatusStates.Installing;
        }

        public bool IsRunning()
        {
            if (_gameServer.Pid == null || _gameServer.Pid == 0)
            {
                return false;
            }

            try
            {
                Process proc = Process.GetProcessById((int)_gameServer.Pid);
                if (!proc.HasExited)
                {
                    return true;
                }
                return false;
                
            } catch (ArgumentException e)
            {
                _logger.LogDebug("Unable to locate PID {pid}", _gameServer.Pid);
                return false;
            }

        }

        public void ReinstallGameServer()
        {
            DeleteGameServer();
            InstallGameServer();
        }

        public void StartServer()
        {
            if (IsRunning())
            {
                _logger.LogDebug("Server {id} is already running with PID {pid}", _gameServer.Id, _gameServer.Pid);
                return;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = _gameServer.CommandLine;
            start.FileName = Path.Combine(_gameServer.HomeDirectory, _gameServer.Game.ExeName);
            start.WindowStyle = ProcessWindowStyle.Hidden;
            Process proc = Process.Start(start);
            _gameServer.Pid = proc.Id;

            _repository.Update(_gameServer);

        }

        public void StopServer()
        {
            
            _logger.LogDebug("Attempting to stop game server");
            if (!IsRunning())
            {
                return;
            }

            try
            {
                Process proc = Process.GetProcessById((int)_gameServer.Pid);
                proc.Kill();
                _gameServer.Pid = null;
                _repository.Update(_gameServer);
                _logger.LogInformation("Killed game server with pid {pid}", _gameServer.Pid);
                return;
            }
            catch (ArgumentException e)
            {
                _logger.LogError("Unable to locate PID {pid}", _gameServer.Pid);
                return;
            }
        }

        public void SetServerStatus()
        {
            // TODO - Logic to detect crashes
            if (!IsRunning())
            {                
                if (gameServerStatus.status != ServerStatusStates.Stopped)
                {
                    _logger.LogInformation("Settings status for game server {id} to stopped", _gameServer.Id); ;
                    gameServerStatus.status = ServerStatusStates.Stopped;                    
                }
                _repository.Update(_gameServer);
                return;
            }

            Process proc = Process.GetProcessById((int)_gameServer.Pid);
            if (proc.HasExited)
            {
                _gameServer.Pid = null;
                if (gameServerStatus.status != ServerStatusStates.Stopped)
                {
                    _logger.LogInformation("Existing PID found for game server {id} but proces has exited", _gameServer.Id);
                    gameServerStatus.status = ServerStatusStates.Stopped;
                }
                    
            } else
            {
                if (gameServerStatus.status != ServerStatusStates.Running)
                {
                    _logger.LogInformation("Setting game server {id} to running state", _gameServer.Id);
                    gameServerStatus.status = ServerStatusStates.Running;
                }
                    
            }
            _repository.Update(_gameServer);
        }

        // TODO - Remove me
        public string InstallationStatus()
        {
            if (gameServerStatus == null)
            {
                return "Install has completed";
            }

            if (gameServerStatus.IsComplete())
            {
                return "Install has completed";
            } else
            {
                return "Install is running";
            }


        }
    }
}
