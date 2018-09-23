using System.Threading.Tasks;
using GhostPanel.Rcon.Steam.Packets;

namespace GhostPanel.Rcon
{
    public interface IQueryProtocol
    {
        Task<ServerInfoBase> GetServerInfoAsync();
        Task<ServerPlayersBase[]> GetServerPlayersAsync();
    }
}