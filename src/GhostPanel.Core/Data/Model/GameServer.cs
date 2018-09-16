﻿
using GhostPanel.Core.GameServerUtils;

namespace GhostPanel.Core.Data.Model
{
    public class GameServer : DataEntity
    {
        public int GameId { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string ServerName { get; set; }
        public bool IsEnabled { get; set; }
        public string Version { get; set; }
        public string StartDirectory { get; set; }
        public string HomeDirectory { get; set; }
        public string CommandLine { get; set; }
        public int? Pid { get; set; }
        public Game Game { get; set; }


    }
}
