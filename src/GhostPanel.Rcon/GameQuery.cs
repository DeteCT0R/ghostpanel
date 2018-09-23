using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon
{
    public class GameQuery<T> where T : IQueryProtocol
    {
        private T _queryProtocol;

        public GameQuery(T queryProtocol)
        {
            _queryProtocol = queryProtocol;
        }
    }
}
