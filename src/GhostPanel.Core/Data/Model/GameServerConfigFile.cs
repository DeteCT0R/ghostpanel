namespace GhostPanel.Core.Data.Model
{
    public class GameServerConfigFile : DataEntity
    {
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string FileContent { get; set; }
        public int GameServerId { get; set; }
        public GameServer GameServer { get; set; }
        public GameDefaultConfigFile GameDefaultConfigFile { get; set; }
    }
}
