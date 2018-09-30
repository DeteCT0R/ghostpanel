using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public class GameServerCurrentStats : DataEntity
    {
        public int? Pid { get; set; }
        public string Map { get; set; }
        public int CurrentPlayers { get; set; } = 0;
        public int MaxPlayers { get; set; } = 0;
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ServerStatusStates Status { get; set; }
        public int RestartAttempts { get; set; } = 0;
        //public int GameServerId { get; set; }
        public GameServer GameServer { get; set; }
    }
}
