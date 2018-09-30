using GhostPanel.Core.Data.Model;
using MediatR;

namespace GhostPanel.Core.Commands
{
    public class UpdateServerStatusCommand : IRequest<CommandResponse>
    {
        public UpdateServerStatusCommand(int gameServerId, ServerStatusStates newState)
        {
            this.gameServerId = gameServerId;
            this.newState = newState;
        }

        public int gameServerId { get; private set; }
        public ServerStatusStates newState { get; private set; }
    }
}
