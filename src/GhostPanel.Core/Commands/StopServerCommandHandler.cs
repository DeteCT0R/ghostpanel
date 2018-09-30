using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;

namespace GhostPanel.Core.Commands
{
    public class StopServerCommandHandler : IRequestHandler<StopServerCommand, CommandResponseGameServer>
    {
        private readonly IMediator _mediator;
        private readonly IServerProcessManager _procManager;
        private readonly IRepository _repository;

        public StopServerCommandHandler(IMediator mediator, IServerProcessManagerProvider procProvider, IRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
            _procManager = procProvider.GetProcessManagerProvider();
        }

        public Task<CommandResponseGameServer> Handle(StopServerCommand request, CancellationToken cancellationToken)
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
                _procManager.StopServer(gameServer);
                gameServer.GameServerCurrentStats.Status = ServerStatusStates.Stopped;
                gameServer.GameServerCurrentStats.Pid = null;
                _repository.Update(gameServer);
                response.status = CommandResponseStatusEnum.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.status = CommandResponseStatusEnum.Error;
                response.message = e.ToString();
            }
            
            return Task.FromResult(response);
        }
    }


}
