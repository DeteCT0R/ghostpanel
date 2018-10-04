using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Communication.Mediator.Commands;
using GhostPanel.Communication.Query;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Communication.Mediator.Handlers.Commands
{
    public class QueryServerCommandHandler : IRequestHandler<QueryServerCommand, ServerStatsWrapper>
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;
        private readonly IGameQueryFactory _gameQueryFactory;

        public QueryServerCommandHandler(IRepository repository, ILogger<QueryServerCommandHandler> logger, IGameQueryFactory gameQueryFactory)
        {
            _repository = repository;
            _logger = logger;
            _gameQueryFactory = gameQueryFactory;
        }

        public async Task<ServerStatsWrapper> Handle(QueryServerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Running Handler QueryGameServerHandler");
            var statsWrapper = new ServerStatsWrapper()
            {
                gameServerId = request.gameServerId
            };
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(request.gameServerId));
            if (gameServer == null)
            {
                _logger.LogError($"Unable to located game server with ID {request.gameServerId}");
                return statsWrapper;
            }

            var query = _gameQueryFactory.GetQueryProtocol(gameServer);
            statsWrapper.players = await query.GetServerPlayersAsync();
            statsWrapper.serverInfo = await query.GetServerInfoAsync();
            
            return statsWrapper;
        }
    }
}
