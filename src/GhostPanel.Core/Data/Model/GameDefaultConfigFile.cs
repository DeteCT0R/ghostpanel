namespace GhostPanel.Core.Data.Model
{
    public class GameDefaultConfigFile : DataEntity
    {
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string Template { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
