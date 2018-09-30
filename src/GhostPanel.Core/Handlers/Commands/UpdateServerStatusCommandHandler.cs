using MediatR;
using GhostPanel.Core.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Commands;

namespace GhostPanel.Core.Handlers.Commands
{
    class UpdateServerStatusCommandHandler: IRequestHandler<UpdateServerStatusCommand, CommandResponseBase>
    {
        private readonly IMediator _mediator;
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public UpdateServerStatusCommandHandler(IMediator mediator, IRepository repository, ILogger<UpdateServerStatusCommandHandler> logger)
        {
            _mediator = mediator;
            _repository = repository;
            _logger = logger;
        }

        public Task<CommandResponseBase> Handle(UpdateServerStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponseBase();
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(request.gameServerId));
            if (gameServer == null)
            {
                response.status = CommandResponseStatusEnum.Error;
                response.message = $"Unable to locate game server with ID {request.gameServerId}";
                return Task.FromResult(response);
            }

            gameServer.GameServerCurrentStats.Status = request.newState;
            _repository.Update(gameServer);

            response.status = CommandResponseStatusEnum.Success;
            response.message = "Game server status updated";

            return Task.FromResult(response);
        }
    }
}
