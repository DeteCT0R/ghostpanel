using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GhostPanel.Communication.Query
{
    class QstatQuery : IQueryProtocol
    {
        public Task<ServerInfoBase> GetServerInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServerPlayersBase[]> GetServerPlayersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
