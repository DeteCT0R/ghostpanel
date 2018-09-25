using System.Threading.Tasks;


namespace GhostPanel.Rcon
{
    public abstract class GameQuery : IQueryProtocol
    {

        public abstract Task<ServerInfoBase> GetServerInfoAsync();
        public abstract Task<ServerPlayersBase[]> GetServerPlayersAsync();

    }
}
