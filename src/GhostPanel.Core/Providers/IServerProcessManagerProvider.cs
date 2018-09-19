using GhostPanel.Core.Management;

namespace GhostPanel.Core.Providers
{
    public interface IServerProcessManagerProvider
    {
        IServerProcessManager GetProcessManagerProvider();
    }
}