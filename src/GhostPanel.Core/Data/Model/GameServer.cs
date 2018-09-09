
using GhostPanel.Core.Games;

namespace GhostPanel.Core.Data.Model
{
    class GameServer
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string ServerName { get; set; }
        public bool IsEnabled { get; set; }
        public string Version { get; set; }
        public string StartPath { get; set; }
        public string HomeDirectory { get; set; }
        public string CommandLine { get; set; }
        public int? LastPid { get; set; }
        public Game Game { get; set; }


    }
}
