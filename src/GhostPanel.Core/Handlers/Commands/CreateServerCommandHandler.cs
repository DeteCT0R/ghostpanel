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
using GhostPanel.Core.Commands;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Handlers.Commands
{
    public class CreateServerCommandHandler : IRequestHandler<CreateServerCommand, CommandResponseGameServer>
    {
        private readonly IMediator _mediator;
        private readonly IGameServerManager _serverManager;
        private readonly IPortAndIpProvider _portProvider;
        private readonly IDefaultDirectoryProvider _dirProvider;
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public CreateServerCommandHandler(IMediator mediator, 
            IGameServerManager serverManager, 
            IPortAndIpProvider portProvider,
            IDefaultDirectoryProvider dirProvider,
            IRepository repository,
            ILogger<CreateServerCommandHandler> logger)
        {
            _mediator = mediator;
            _serverManager = serverManager;
            _portProvider = portProvider;
            _dirProvider = dirProvider;
            _repository = repository;
            _logger = logger;
        }

        public Task<CommandResponseGameServer> Handle(CreateServerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Running Handler CreateServerCommandHandler");
            var response = new CommandResponseGameServer() ;
            var gameServer = request.gameServer;
            var game = _repository.Single(DataItemPolicy<Game>.ById(gameServer.GameId));
            if (game == null)
            {
                response.status = CommandResponseStatusEnum.Error;
                response.message = $"Unable to locate game with ID {gameServer.GameId}";
                _mediator.Publish(new ServerInstallStatusNotification("error", $"Unable to locate game with ID {gameServer.GameId}"));
                return Task.FromResult(response);
            }

            gameServer.GameConfigFiles.Add();

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
                response.status = CommandResponseStatusEnum.Success;
                response.message = e.ToString();
                _mediator.Publish(new ServerInstallStatusNotification("error", e.ToString()));
                return Task.FromResult(response);
            }
            
            try
            {
                _serverManager.InstallGameServer(gameServer);
                _mediator.Publish(new ServerInstallStatusNotification("installing", $"Game server install started for server {gameServer.Guid}"));
                response.status = CommandResponseStatusEnum.Success;
                response.message = "Game server now installing";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.status = CommandResponseStatusEnum.Error;
                response.message = e.ToString();
                _mediator.Publish(new ServerInstallStatusNotification("error", e.ToString()));
            }



            return Task.FromResult(response);


        }
    }
}
