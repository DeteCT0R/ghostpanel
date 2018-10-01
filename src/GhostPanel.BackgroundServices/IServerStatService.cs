using System.Threading.Tasks;
using GhostPanel.Communication.Query;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.BackgroundServices
{
    public interface IServerStatService
    {
        GameServer CheckServerProc(GameServer gameServer);
        Task<ServerStatsWrapper> UpdateServerQueryStatsAsync(GameServer gameServer);
    }
}