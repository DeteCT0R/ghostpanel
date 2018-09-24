using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public class GameServerCurrentStat : DataEntity
    {
        public int? Pid { get; set; }
        public string Map { get; set; }
        public int CurrentPlayer { get; set; }
        public int MaxPlayers { get; set; }
        public string Name { get; set; }
        public ServerStatusStates Status { get; set; }
        public int RestartAttempts { get; set; }
        public GameServer Server { get; set; }
        public int ServerId { get; set; }
    }
}
