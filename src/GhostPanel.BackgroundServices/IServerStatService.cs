using System.Threading.Tasks;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.BackgroundServices
{
    public interface IServerStatService
    {
        GameServer CheckServerProc(GameServer gameServer);
        Task<GameServer> UpdateServerQueryStatsAsync(GameServer gameServer);
    }
}