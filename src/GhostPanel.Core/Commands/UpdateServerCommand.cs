using MediatR;

namespace GhostPanel.Core.Commands
{
    public class UpdateServerCommand : IRequest<CommandResponseGameServer>
    {
        public UpdateServerCommand(int gameServerId)
        {
            this.gameServerId = gameServerId;
        }

        public int gameServerId { get; private set; }


    }
}
