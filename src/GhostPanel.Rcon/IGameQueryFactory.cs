using GhostPanel.Core.Data.Model;

namespace GhostPanel.Rcon
{
    public interface IGameQueryFactory
    {
        IQueryProtocol GetQueryProtocol(GameServer gameServer);
    }
}