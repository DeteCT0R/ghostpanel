using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Commands;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.GameServerUtils;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Handlers.Commands
{
    class UpdateServerCommandHandler : IRequestHandler<UpdateServerCommand, CommandResponseGameServer>
    {
        private readonly ILogger _logger;
        private readonly IGameServerManager _serverManager;
        private readonly IRepository _repository;

        public UpdateServerCommandHandler(ILogger<UpdateServerCommandHandler> logger, IGameServerManager serverManager, IRepository repository)
        {
            _logger = logger;
            _serverManager = serverManager;
            _repository = repository;
        }

        public Task<CommandResponseGameServer> Handle(UpdateServerCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponseGameServer();
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(request.gameServerId));
            var game = _repository.Single(DataItemPolicy<Game>.ById(gameServer.GameId));
            _repository.Single(DataItemPolicy<GameServerCurrentStats>.ById(request.gameServerId));
            if (gameServer == null)
            {
                response.status = CommandResponseStatusEnum.Error;
                response.message = $"Unable located game server with ID {request.gameServerId}";
                return Task.FromResult(response);
            }

            _serverManager.InstallGameServer(gameServer);
            gameServer.GameServerCurrentStats.Status = ServerStatusStates.Updating;
            _repository.Update(gameServer);
            response.status = CommandResponseStatusEnum.Success;
            response.message = "Server is now updating";

            return Task.FromResult(response);
        }
    }
}
