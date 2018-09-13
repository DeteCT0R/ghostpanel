using GhostPanel.Core.GameServerUtils;

namespace GhostPanel.Core.Managment
{
    public interface IServerManagerContainer
    {
        void AddServerManager(GameServerManager manager);
        void RemoveServerManager(GameServerManager manager);
        void RefreshServerStatus();
    }
}