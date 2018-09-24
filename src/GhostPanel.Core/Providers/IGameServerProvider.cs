using System.Collections.Generic;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Providers
{
    public interface IGameServerProvider
    {
        List<GameServer> GetGameServers();
    }
}