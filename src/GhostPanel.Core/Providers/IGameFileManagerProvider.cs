using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Managment.GameFiles;

namespace GhostPanel.Core.Providers
{
    public interface IGameFileManagerProvider
    {
        IGameFileManager GetGameFileManager(GameServer gameServer);
    }
}