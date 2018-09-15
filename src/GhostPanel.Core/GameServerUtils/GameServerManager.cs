using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management;
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

        public GameServer gameServer;
        private IGameFileManager _gameFileManager;
        private readonly IRepository _repository;
        public GameServerStatus gameServerStatus;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _logfactory;
        private readonly SteamCredentialWrapper _steamCmd;

        public GameServerManager(IRepository repository, SteamCredentialWrapper steamCmd, ILoggerFactory logger)
        {
            _repository = repository;
            _logger = logger.CreateLogger<GameServerManager>();
            _logfactory = logger;
            _steamCmd = steamCmd;
            gameServerStatus = new GameServerStatus() { status = ServerStatusStates.Unknown };
        }


        private void InitGameFileManager()
        {
            if (gameServer.Game.SteamAppId != null)
            {
                _gameFileManager = new SteamCmdGameFiles(_steamCmd, _logfactory, gameServer.Game.SteamAppId, gameServer.HomeDirectory);
            } else
            {
                _gameFileManager = new FileServerGameFiles(gameServer.HomeDirectory, gameServer.Game.ArchiveName);
            }
        }

        /// <summary>
        /// Allows the game server to be set after creation.  This lets the GameServerManager be created via dependancy injection
        /// </summary>
        /// <param name="gameServer"></param>
        public void SetGameServer(GameServer gameServer)
        {
            this.gameServer = gameServer;
            InitGameFileManager();
        }

        public void DeleteGameServer()
        {
            StopServer();
            try
            {
                Directory.Delete(gameServer.HomeDirectory, true);
            } catch (Exception e)
            {
                _logger.LogError("Failed to delete game server {id}", gameServer.Id);
            }
        }

        public void InstallGameServer()
        {
            // TODO - Check if there's an active install             
            gameServerStatus.status = ServerStatusStates.Installing;
            _gameFileManager.DownloadGameServerFiles();
        }

        public bool IsRunning()
        {
            if (gameServer.Pid == null || gameServer.Pid == 0)
            {
                return false;
            }

            try
            {
                Process proc = Process.GetProcessById((int)gameServer.Pid);
                if (!proc.HasExited)
                {
                    return true;
                }
                return false;
                
            } catch (ArgumentException e)
            {
                _logger.LogDebug("Unable to locate PID {pid}", gameServer.Pid);
                return false;
            }

        }

        public void ReinstallGameServer()
        {
            StopServer();
            DeleteGameServer();
            InstallGameServer();
        }

        public void StartServer()
        {
            if (IsRunning())
            {
                _logger.LogDebug("Server {id} is already running with PID {pid}", gameServer.Id, gameServer.Pid);
                return;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = gameServer.CommandLine;
            start.FileName = Path.Combine(gameServer.HomeDirectory, gameServer.Game.ExeName);
            start.WindowStyle = ProcessWindowStyle.Hidden;
            Process proc = Process.Start(start);
            gameServer.Pid = proc.Id;

            _repository.Update(gameServer);

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
                Process proc = Process.GetProcessById((int)gameServer.Pid);
                proc.Kill();
                gameServer.Pid = null;
                _repository.Update(gameServer);
                _logger.LogInformation("Killed game server with pid {pid}", gameServer.Pid);
                return;
            }
            catch (ArgumentException e)
            {
                _logger.LogError("Unable to locate PID {pid}", gameServer.Pid);
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
                    _logger.LogInformation("Settings status for game server {id} to stopped", gameServer.Id); ;
                    gameServerStatus.status = ServerStatusStates.Stopped;                    
                }
                _repository.Update(gameServer);
                return;
            }

            Process proc = Process.GetProcessById((int)gameServer.Pid);
            if (proc.HasExited)
            {
                gameServer.Pid = null;
                if (gameServerStatus.status != ServerStatusStates.Stopped)
                {
                    _logger.LogInformation("Existing PID found for game server {id} but proces has exited", gameServer.Id);
                    gameServerStatus.status = ServerStatusStates.Stopped;
                }
                    
            } else
            {
                if (gameServerStatus.status != ServerStatusStates.Running)
                {
                    _logger.LogInformation("Setting game server {id} to running state", gameServer.Id);
                    gameServerStatus.status = ServerStatusStates.Running;
                }
                    
            }
            _repository.Update(gameServer);
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
