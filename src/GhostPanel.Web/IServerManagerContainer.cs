using GhostPanel.Core.GameServerUtils;

namespace GhostPanel.Web
{
    public interface IServerManagerContainer
    {
        void AddServerManager(GameServerManager manager);
        void RemoveServerManager(GameServerManager manager);
    }
}