using GhostPanel.Core.GameServerUtils;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Providers;
using System.IO;
using GhostPanel.Core.Notifications;
using GhostPanel.Core.Data.Specifications;

namespace GhostPanel.Core.Commands
{
    public class CreateServerCommandHandler : IRequestHandler<CreateServerCommand, CommandResponse>
    {
        private readonly IMediator _mediator;
        private readonly IGameServerManager _serverManager;
        private readonly IPortAndIpProvider _portProvider;
        private readonly IDefaultDirectoryProvider _dirProvider;
        private readonly IRepository _repository;

        public CreateServerCommandHandler(IMediator mediator, 
            IGameServerManager serverManager, 
            IPortAndIpProvider portProvider,
            IDefaultDirectoryProvider dirProvider,
            IRepository repository)
        {
            _mediator = mediator;
            _serverManager = serverManager;
            _portProvider = portProvider;
            _dirProvider = dirProvider;
            _repository = repository;
        }

        public Task<CommandResponse> Handle(CreateServerCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            var gameServer = request.gameServer;
            var game = _repository.Single(DataItemPolicy<Game>.ById(gameServer.GameId));
            if (game == null)
            {
                response.status = "error";
                response.payload = $"Unable to locate game with ID {gameServer.GameId}";
                _mediator.Publish(new ServerInstallStatusNotification("error", $"Unable to locate game with ID {gameServer.GameId}"));
                return Task.FromResult(response);
            }

            gameServer.GamePort = _portProvider.GetNextAvailablePort(game.GamePort, gameServer.IpAddress, game.PortIncrement);
            gameServer.QueryPort = _portProvider.GetNextAvailablePort(game.QueryPort, gameServer.IpAddress, game.PortIncrement);
            gameServer.HomeDirectory = Path.Combine(_dirProvider.GetBaseInstallDirectory(), gameServer.Guid.ToString());
            gameServer.GameServerCurrentStats = new GameServerCurrentStats();
            gameServer.GameServerCurrentStats.Status = ServerStatusStates.Installing;
            try
            {
                _repository.Create(gameServer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.status = "error";
                response.payload = e.ToString();
                _mediator.Publish(new ServerInstallStatusNotification("error", e.ToString()));
                return Task.FromResult(response);
            }
            
            try
            {
                _serverManager.InstallGameServer(gameServer);
                _mediator.Publish(new ServerInstallStatusNotification("installing", $"Game server install started for server {gameServer.Guid}"));
                response.status = "installing";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.status = "Error";
                response.payload = e.ToString();
                _mediator.Publish(new ServerInstallStatusNotification("error", e.ToString()));
            }

            return Task.FromResult(response);

        }
    }
}
