using GhostPanel.Core.Data.Model;
using MediatR;

namespace GhostPanel.Core.Commands
{
    public class StopServerCommand : IRequest<CommandResponseGameServer>
    {
        public int gameServerId { get; private set; }

        public StopServerCommand(int gameServerId)
        {
            this.gameServerId = gameServerId;
        }
    }
}
