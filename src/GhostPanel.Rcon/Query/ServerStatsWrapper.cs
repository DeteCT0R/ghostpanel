namespace GhostPanel.Communication.Query
{
    public class ServerStatsWrapper
    {
        public ServerStatsWrapper()
        {
        }

        public ServerStatsWrapper(ServerInfoBase serverInfo, ServerPlayersBase[] players, int gameServerId)
        {
            this.serverInfo = serverInfo;
            this.players = players;
            this.gameServerId = gameServerId;
        }

        public ServerInfoBase serverInfo { get; set; }
        public ServerPlayersBase[] players { get; set; }
        public int gameServerId { get; set; }
        public object GameServerCurrentStats { get; set; }
    }
}
