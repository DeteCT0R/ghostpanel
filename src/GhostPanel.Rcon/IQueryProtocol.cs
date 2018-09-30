using System.Threading.Tasks;
using GhostPanel.Rcon.Steam.Packets;

namespace GhostPanel.Communication.Query
{
    public interface IQueryProtocol
    {
        Task<ServerInfoBase> GetServerInfoAsync();
        Task<ServerPlayersBase[]> GetServerPlayersAsync();
    }
}