using GhostPanel.Communication.Query;
using MediatR;

namespace GhostPanel.Communication.Mediator.Commands
{
    public class QueryServerCommand : IRequest<ServerStatsWrapper>
    {
        public int gameServerId;

        public QueryServerCommand(int gameServerId)
        {
            this.gameServerId = gameServerId;
        }
    }
}
