using MediatR;

namespace GhostPanel.Core.Commands
{
    public class StartServerCommand : IRequest<CommandResponseGameServer>
    {
        public StartServerCommand(int gameServerId)
        {
            this.gameServerId = gameServerId;
        }

        public int gameServerId { get; private set; }

    }
}
