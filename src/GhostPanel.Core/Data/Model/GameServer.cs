
using System;
using System.Collections.Generic;
using GhostPanel.Core.GameServerUtils;

namespace GhostPanel.Core.Data.Model
{
    public class GameServer : DataEntity
    {
        public int GameId { get; set; }
        public string IpAddress { get; set; }
        public int GamePort { get; set; }
        public int QueryPort { get; set; }
        public string ServerName { get; set; }
        public bool IsEnabled { get; set; }
        public string Version { get; set; }
        public string StartDirectory { get; set; }
        public string HomeDirectory { get; set; }
        public string CommandLine { get; set; }
        public int Slots { get; set; }
        public string RconPassword { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public Dictionary<string, string> CustomCommandLineArgs { get; set; }
        public Game Game { get; set; }
        public GameServerCurrentStats GameServerCurrentStats { get; set; }
        



    }
}
