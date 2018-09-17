using System;
using System.Linq.Expressions;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Data.Specifications
{
    class GameServerPolicy : DataItemPolicy<GameServer>
    {
        protected GameServerPolicy(Expression<Func<GameServer, bool>> expression) : base(expression)
        {

        }

        public static GameServerPolicy ByGamePort(int port)
        {
            return new GameServerPolicy(x => x.GamePort == port);
        }

        public static GameServerPolicy ByGamePort(int port, string ip)
        {
            return new GameServerPolicy(x => x.GamePort == port && x.IpAddress == ip);
        }

        public static GameServerPolicy ByQueryPort(int port)
        {
            return new GameServerPolicy(x => x.GamePort == port);
        }

        public static GameServerPolicy ByQueryPort(int port, string ip)
        {
            return new GameServerPolicy(x => x.GamePort == port && x.IpAddress == ip);
        }
    }
}
