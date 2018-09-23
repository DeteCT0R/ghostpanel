using System.Threading.Tasks;
using GhostPanel.Rcon.Steam.Packets;

namespace GhostPanel.Rcon
{
    public interface IQueryProtocol
    {
        As2InfoResponsePacket GetServerInfo();
        Task<As2InfoResponsePacket> GetServerInfoAsync();
        A2SPlayerResponsePacket[] GetServerPlayers();
        Task<A2SPlayerResponsePacket[]> GetServerPlayersAsync();
    }
}