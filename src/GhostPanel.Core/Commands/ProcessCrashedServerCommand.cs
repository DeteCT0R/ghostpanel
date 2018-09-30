using MediatR;

namespace GhostPanel.Core.Commands
{
    public class ProcessCrashedServerCommand : IRequest<CommandResponseGameServer>
    {
        public ProcessCrashedServerCommand(int gameServerId)
        {
            this.gameServerId = gameServerId;
        }

        public int gameServerId { get; private set; }

    }
}
