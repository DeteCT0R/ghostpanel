namespace GhostPanel.Core.Managment.GameFiles
{
    public interface IGameFileManager
    {
        void DownloadGameServerFiles();
        void DeleteGameServerFiles();
        void UpdateGameServerFiles();
        int GetInstallProgress();

    }
}
