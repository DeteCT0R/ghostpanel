using System;
using System.Net;
using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Rcon
{
    public class GameQueryFactory : IGameQueryFactory
    {
        private readonly ILoggerFactory _logger;

        public GameQueryFactory(ILoggerFactory logger)
        {
            _logger = logger;
        }

        public IQueryProtocol GetQueryProtocol(GameServer gameServer)
        {
            var queryType = Type.GetType(gameServer.Protocol.FullTypeName);
            if (queryType == null)
            {
                return null;
            }

            return Activator.CreateInstance(queryType,
                new IPEndPoint(IPAddress.Parse(gameServer.IpAddress), gameServer.QueryPort), _logger) as IQueryProtocol;
        }
    }
}
