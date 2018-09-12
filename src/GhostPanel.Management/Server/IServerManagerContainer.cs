using GhostPanel.Core.GameServerUtils;

namespace GhostPanel.Management.Server
{
    public interface IServerManagerContainer
    {
        void AddServerManager(GameServerManager manager);
        void RemoveServerManager(GameServerManager manager);
        void RefreshServerStatus();
    }
}