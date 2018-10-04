using GhostPanel.Core.Data.Model;
using System;
using System.Linq.Expressions;

namespace GhostPanel.Core.Data.Specifications
{
    public class GameServerConfigFilePolicy : DataItemPolicy<GameServerConfigFile>
    {
        protected GameServerConfigFilePolicy(Expression<Func<GameServerConfigFile, bool>> expression) : base(expression)
        {

        }

        public static GameServerConfigFilePolicy ByServerId(int id)
        {
            return new GameServerConfigFilePolicy(x => x.GameServerId == id);
        }
    }
}
