using GhostPanel.Core.Data.Model;
using MediatR;

namespace GhostPanel.Core.Commands
{
    public class RestartServerCommand : IRequest<CommandResponse>
    {
        public GameServer gameServer { get; private set; }

        public RestartServerCommand(GameServer gameServer)
        {
            this.gameServer = gameServer ?? throw new System.ArgumentNullException(nameof(gameServer));
        }
    }
}
