using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Commands
{
    class UpdateServerStatusCommandHandler: IRequestHandler<UpdateServerStatusCommand, CommandResponse>
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

        public Task<CommandResponse> Handle(UpdateServerStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(request.gameServerId));
            if (gameServer == null)
            {
                response.status = "error";
                response.payload = $"Unable to locate game server with ID {request.gameServerId}";
                return Task.FromResult(response);
            }

            gameServer.GameServerCurrentStats.Status = request.newState;
            _repository.Update(gameServer);

            response.status = "complete";
            response.payload = "Game server status updated";

            return Task.FromResult(response);
        }
    }
}
