using System;
using System.Net;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Rcon
{
    public class GameQueryFactory : IGameQueryFactory
    {
        private readonly ILoggerFactory _logger;
        private readonly IRepository _repository;

        public GameQueryFactory(ILoggerFactory logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IQueryProtocol GetQueryProtocol(GameServer gameServer)
        {
            var queryProto = _repository.Single(DataItemPolicy<GameProtocol>.ById(gameServer.Game.GameProtocolId));
            var queryType = Type.GetType(queryProto.FullTypeName);
            if (queryType == null)
            {
                return null;
            }

            return Activator.CreateInstance(queryType,
                new IPEndPoint(IPAddress.Parse(gameServer.IpAddress), gameServer.QueryPort), _logger) as IQueryProtocol;
        }
    }
}