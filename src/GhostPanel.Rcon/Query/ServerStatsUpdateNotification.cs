using MediatR;

namespace GhostPanel.Communication.Query
{
    public class ServerStatsUpdateNotification : INotification
    {
        public ServerStatsUpdateNotification(ServerStatsWrapper serverStats)
        {
            ServerStats = serverStats;
        }

        public ServerStatsWrapper ServerStats { get; set; }
    }
}
