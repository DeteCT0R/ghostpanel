namespace GhostPanel.Communication.Query
{
    public abstract class ServerInfoBase
    {
        public string Game { get; set; }
        public string Map { get; set; }
        public int MaxPlayers { get; set; }
        public int CurrentPlayers { get; set; }

    }
}
