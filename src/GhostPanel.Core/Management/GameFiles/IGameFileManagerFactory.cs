using GhostPanel.Core.Managment.GameFiles;

namespace GhostPanel.Core.Management.GameFiles
{
    public interface IGameFileManagerFactory
    {
        
        IGameFileManager GetGameFileManager(string installDir, int appId);

    }
}
