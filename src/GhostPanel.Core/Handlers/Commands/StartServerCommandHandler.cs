using System;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Commands;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Handlers.Commands
{
    public class StartServerCommandHandler : IRequestHandler<StartServerCommand, CommandResponseGameServer>
    {
        private readonly IMediator _mediator;
        private readonly IServerProcessManager _procManager;
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public StartServerCommandHandler(IMediator mediator, IServerProcessManagerProvider procProvider, IRepository repository, ILogger<StartServerCommandHandler> logger)
        {
            _mediator = mediator;
            _repository = repository;
            _logger = logger;
            _procManager = procProvider.GetProcessManagerProvider();
        }

        public Task<CommandResponseGameServer> Handle(StartServerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Running Handler RestartServerCommandHandler");
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
            try
            {
                var proc = _procManager.StartServer(gameServer);
                response.status = CommandResponseStatusEnum.Success;
                gameServer.GameServerCurrentStats.Pid = proc.Id;
                gameServer.GameServerCurrentStats.Status = ServerStatusStates.Running;
                _repository.Update(gameServer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.status = CommandResponseStatusEnum.Error;
                response.message = e.ToString();
                gameServer.GameServerCurrentStats.Status = ServerStatusStates.Error;
                _repository.Update(gameServer);
            }

            return Task.FromResult(response);
        }

    }
}
