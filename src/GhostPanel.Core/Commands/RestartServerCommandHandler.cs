using System;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using MediatR;

namespace GhostPanel.Core.Commands
{
    public class RestartServerCommandHandler : IRequestHandler<RestartServerCommand, CommandResponseGameServer>
    {
        private readonly IMediator _mediator;
        private readonly IServerProcessManager _procManager;
        private readonly IRepository _repository;

        public RestartServerCommandHandler(IMediator mediator, IServerProcessManagerProvider procProvider, IRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
            _procManager = procProvider.GetProcessManagerProvider();
        }

        public Task<CommandResponseGameServer> Handle(RestartServerCommand request, CancellationToken cancellationToken)
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
            try
            {
                var proc = _procManager.RestartServer(gameServer);
                response.status = CommandResponseStatusEnum.Success;
                gameServer.GameServerCurrentStats.Pid = proc.Id;
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
