using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Managment.GameFiles
{
    public interface IGameFileManager
    {
        void DownloadGameServerFiles(GameServer gameServer);
        void DeleteGameServerFiles(GameServer gameServer);
        void UpdateGameServerFiles();

    }
}
