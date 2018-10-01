using GhostPanel.Core.Data.Model;

namespace GhostPanel.Communication.Query
{
    public interface IGameQueryFactory
    {
        IQueryProtocol GetQueryProtocol(GameServer gameServer);
    }
}