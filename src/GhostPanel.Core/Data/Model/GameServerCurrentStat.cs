using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public class GameServerCurrentStat : DataEntity
    {
        public int? Pid { get; set; }
        public string Map { get; set; }
        public int CurrentPlayers { get; set; } = 0;
        public int MaxPlayers { get; set; } = 0;
        public string Name { get; set; }
        public ServerStatusStates Status { get; set; }
        public int RestartAttempts { get; set; } = 0;
        public GameServer Server { get; set; }
        public int ServerId { get; set; }
    }
}
