using System.Threading.Tasks;


namespace GhostPanel.Communication.Query
{
    public abstract class GameQuery : IQueryProtocol
    {

        public abstract Task<ServerInfoBase> GetServerInfoAsync();
        public abstract Task<ServerPlayersBase[]> GetServerPlayersAsync();

    }
}
