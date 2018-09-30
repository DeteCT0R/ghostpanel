using GhostPanel.Core.Data.Model;
using MediatR;

namespace GhostPanel.Core.Commands
{
    public class RestartServerCommand : IRequest<CommandResponseGameServer>
    {
        public int gameServerId { get; private set; }

        public RestartServerCommand(int gameServerId)
        {
            this.gameServerId = gameServerId;
        }
    }
}
