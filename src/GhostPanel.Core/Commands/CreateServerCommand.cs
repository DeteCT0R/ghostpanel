using GhostPanel.Core.Data.Model;
using MediatR;

namespace GhostPanel.Core.Commands
{
    public class CreateServerCommand : IRequest<CommandResponse>
    {
        public GameServer gameServer { get; private set; }

        public CreateServerCommand(GameServer gameServer)
        {
            this.gameServer = gameServer ?? throw new System.ArgumentNullException(nameof(gameServer));
        }
    }
}
