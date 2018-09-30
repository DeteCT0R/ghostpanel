using GhostPanel.Rcon;

namespace GhostPanel.Communication.Query
{
    public class ServerStatsWrapper
    {
        public ServerStatsWrapper()
        {
        }

        public ServerStatsWrapper(ServerInfoBase serverInfo, ServerPlayersBase[] players)
        {
            this.serverInfo = serverInfo;
            this.players = players;
        }

        public ServerInfoBase serverInfo { get; private set; }
        public ServerPlayersBase[] players { get; private set; }
    }
}
