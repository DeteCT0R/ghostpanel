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

        private GameServer gameServer;
        private SteamCmd steamCmd;
        private int? pid;
        private DbContext _dbContext;

        public GameServerManager(GameServer gameServer, SteamCmd steamCmd, DbContext dbContext)
        {
            GameServer = gameServer;
            SteamCmd = steamCmd;
            _dbContext = dbContext;
            Pid = gameServer.Pid;
        }

        public int ?Pid { get => pid; set => pid = value; }
        internal GameServer GameServer { get => gameServer; set => gameServer = value; }
        internal SteamCmd SteamCmd { get => steamCmd; set => steamCmd = value; }

        public void DeleteGameServer()
        {
            Log.Information("Attempting to delete game server with ID {id}", gameServer.Id);
            try
            {
                Directory.Delete(gameServer.HomeDirectory, true);
            } catch (Exception e)
            {
                Log.Error("Failed to delete game directory");
            }
        }

        public void InstallGameServer()
        {
            steamCmd.downloadGame(gameServer.HomeDirectory, gameServer.Game.SteamAppId);
        }

        public bool IsRunning()
        {
            if (pid == null || pid == 0)
            {
                Log.Information("No PID currently set for server");
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
                
            } catch (ArgumentException e)
            {
                Log.Error("Unable to locate PID {pid}", pid);
                return false;
            }

        }

        public string RefreshStatus()
        {
            if (IsRunning())
            {
                
            }

            return "";
        }

        public void ReinstallGameServer()
        {
            Log.Information("Reinstalling Game Server {id}", gameServer.Id);
            DeleteGameServer();
            InstallGameServer();
        }

        public void StartServer()
        {
            if (IsRunning())
            {
                Log.Information("Server is already running with PID {pid}", pid);
                return;
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = gameServer.CommandLine;
            start.FileName = Path.Combine(gameServer.HomeDirectory, gameServer.Game.ExeName);
            start.WindowStyle = ProcessWindowStyle.Hidden;
            Process proc = Process.Start(start);
            pid = proc.Id;
            gameServer.Pid = pid;

            _dbContext.SaveChanges();

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
                Process proc = Process.GetProcessById((int)pid);
                proc.Kill();
                gameServer.Pid = 0;
                _dbContext.SaveChanges();
                Log.Information("Killed game server with pid {pid}", Pid);
                return;
            }
            catch (ArgumentException e)
            {
                Log.Error("Unable to locate PID {pid}", pid);
                return;
            }
        }
    }
}
