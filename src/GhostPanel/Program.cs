using GameHostDemo.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.IO;
using System.Linq;

namespace GameHostDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

            var context = new GameHostDemoContext();
            var server = context.GameServers.Where(s => s.Id == 1).Include(s => s.Game).FirstOrDefault();
            var steamCmd = new SteamCmd("anonymous", "");

            var manager = new GameServerManager(server, steamCmd, context);

            manager.ReinstallGameServer();
            
            manager.StartServer();
            manager.StopServer();

            manager.InstallGameServer();

            Console.WriteLine("");

            /*
            string steamCmdPath = Path.Join(Directory.GetCurrentDirectory(), "SteamCMD\\steamcmd.exe");
            string installDir = Path.Join(Directory.GetCurrentDirectory(), "Test Server");
            Game counterStrike = new Game();
            SteamCmd steamCmd = new SteamCmd("anonymous", "");
            GameInstaller installer = new GameInstaller(counterStrike, steamCmd, installDir);
            installer.installGame();
            */
        }
    }
}
