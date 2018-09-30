using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Commands
{
    public class ProcessCrashedServerCommandHandler : IRequestHandler<ProcessCrashedServerCommand, CommandResponseGameServer>
    {
        private readonly IMediator _mediator;
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public ProcessCrashedServerCommandHandler(IMediator mediator, IRepository repository, ILogger<ProcessCrashedServerCommandHandler> logger)
        {
            _mediator = mediator;
            _repository = repository;
            _logger = logger;
        }

        public Task<CommandResponseGameServer> Handle(ProcessCrashedServerCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponseGameServer();
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(request.gameServerId));
            _repository.Single(DataItemPolicy<GameServerCurrentStats>.ById(request.gameServerId));
            if (gameServer == null)
            {
                response.status = CommandResponseStatusEnum.Error;
                response.message = $"Unable located game server with ID {request.gameServerId}";
                return Task.FromResult(response);
            }

            response.payload = gameServer;

            if (gameServer.GameServerCurrentStats.Status != ServerStatusStates.Crashed)
            {
                _logger.LogDebug("Game server {id} has a PID set but is not running.  Marking as crashed", gameServer.Id);
                gameServer.GameServerCurrentStats.Status = ServerStatusStates.Crashed;
                _repository.Update(gameServer);

            }

            if (gameServer.GameServerCurrentStats.RestartAttempts < 3) // TODO: Move max restarts to config
            {
                gameServer.GameServerCurrentStats.RestartAttempts++;
                _logger.LogDebug("Attempt #{attempt} to restart server {id}", gameServer.GameServerCurrentStats.RestartAttempts, gameServer.Id);
                _mediator.Send(new RestartServerCommand(request.gameServerId));
                _repository.Update(gameServer);
            }
            else
            {
                _logger.LogDebug("Server {id} has hit the max restart attempts.  Stopping server", gameServer.Id);
                gameServer.GameServerCurrentStats.Status = ServerStatusStates.Stopped;
                gameServer.GameServerCurrentStats.Pid = null;
                _mediator.Send(new StopServerCommand(request.gameServerId));
                _repository.Update(gameServer);
            }

            return Task.FromResult(response);
        }
    }
}
