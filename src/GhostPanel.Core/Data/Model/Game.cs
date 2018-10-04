using System.Collections.Generic;

namespace GhostPanel.Core.Data.Model
{
    public class Game : DataEntity
    {
        public string Name { get; set; }
        public int? SteamAppId { get; set; }
        public string ArchiveName { get; set; }
        public string SteamUrl { get; set; }
        public string ExeName { get; set; }
        public int MaxSlots { get; set; }
        public int MinSlots { get; set; }
        public int DefaultSlots { get; set; }
        public string DefaultPath { get; set; }
        public int GamePort { get; set; }
        public int QueryPort { get; set; }
        public int PortIncrement { get; set; }
        public int GameProtocolId { get; set; }
        public GameProtocol GameProtocol { get; set; }
        public ICollection<GameDefaultConfigFile> GameDefaultConfigFiles {get; set; }
        public ICollection<GameServer> GameServers { get; set; }


    }
}
