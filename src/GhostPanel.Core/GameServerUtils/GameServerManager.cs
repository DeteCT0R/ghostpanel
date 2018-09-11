using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;

namespace GhostPanel.Core.GameServerUtils
{
    public class GameServerManager : IGameServerManager
    {

        public GameServer gameServer;
        private readonly SteamCmd _steamCmd;
        private readonly IRepository _repository;
        public GameServerStatus gameServerStatus;

        public GameServerManager(GameServer gameServer, SteamCmd steamCmd, IRepository repository)
        {
            this.gameServer = gameServer;
            _steamCmd = steamCmd;
            _repository = repository;
            gameServerStatus = new GameServerStatus() { status = ServerStatusStates.Unknown };
        }

        public void DeleteGameServer()
        {
            try
            {
                Directory.Delete(gameServer.HomeDirectory, true);
            } catch (Exception e)
            {
                Console.WriteLine("Failed to Delete Game Server");
            }
        }

        public void InstallGameServer()
        {
            // TODO - Check if there's an active install 
            Process proc = _steamCmd.downloadGame(gameServer.HomeDirectory, gameServer.Game.SteamAppId);
            gameServerStatus.ServerProcess = proc;
            gameServerStatus.status = ServerStatusStates.Installing;
        }

        public bool IsRunning()
        {
            if (gameServer.Pid == null || gameServer.Pid == 0)
            {
                Console.WriteLine("No Pid Set");
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
                Console.WriteLine("Unable to locate PID " + gameServer.Pid);
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
                Console.WriteLine("Server is already running with PID " + gameServer.Pid);
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
            
            Log.Debug("Attempting to stop game server");
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
                Console.WriteLine("Killed game server with pid " + gameServer.Pid);
                return;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Unable to locate PID " + gameServer.Pid);
                return;
            }
        }

        public void SetServerStatus()
        {
            if (!IsRunning())
            {
                gameServerStatus.status = ServerStatusStates.Stopped;
                return;
            }

            Process proc = Process.GetProcessById((int)gameServer.Pid);
            if (proc.HasExited)
            {
                gameServer.Pid = null;
                gameServerStatus.status = ServerStatusStates.Stopped;
            } else
            {
                gameServerStatus.status = ServerStatusStates.Running;
            }
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
