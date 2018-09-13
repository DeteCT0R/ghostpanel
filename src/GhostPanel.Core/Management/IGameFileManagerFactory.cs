using GhostPanel.Core.Managment;

namespace GhostPanel.Core.Management
{
    public interface IGameFileManagerFactory
    {
        
        IGameFileManager GetGameFileManager(string installDir, int appId);

    }
}
