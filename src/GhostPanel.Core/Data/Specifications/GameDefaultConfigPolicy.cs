using System;
using System.Linq.Expressions;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Data.Specifications
{
    public class GameDefaultConfigPolicy : DataItemPolicy<GameDefaultConfigFile>
    {
        protected GameDefaultConfigPolicy(Expression<Func<GameDefaultConfigFile, bool>> expression) : base(expression)
        {

        }

        public static GameDefaultConfigPolicy ByGameId(int id)
        {
            return new GameDefaultConfigPolicy(x => x.GameId == id);
        }
    }
}
