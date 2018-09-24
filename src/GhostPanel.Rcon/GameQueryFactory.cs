using System;
using System.Net;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Rcon
{
    public class GameQueryFactory : IGameQueryFactory
    {
        public IQueryProtocol GetQueryProtocol(GameServer gameServer)
        {
            var queryType = Type.GetType(gameServer.Protocol.FullTypeName);
            if (queryType == null)
            {
                return null;
            }

            return Activator.CreateInstance(queryType,
                new IPEndPoint(IPAddress.Parse(gameServer.IpAddress), gameServer.QueryPort)) as IQueryProtocol;
        }
    }
}
