using System.Threading.Tasks;


namespace GhostPanel.Rcon
{
    public abstract class GameQuery : IQueryProtocol
    {

        public abstract Task<ServerInfoBase> GetServerInfoAsync();
        public abstract Task<ServerPlayersBase[]> GetServerPlayersAsync();

        /*
        private IQueryProtocol _serverQuery;

        public GameQuery(IQueryProtocol serverQuery)
        {
            _serverQuery = serverQuery;
        }


        
        public async Task<ServerInfoBase> GetServerInfoAsync()
        {
            var result = await _serverQuery.GetServerInfoAsync();
            return result;
        }

        public async Task<ServerPlayersBase[]> GetServerPlayersAsync()
        {
            var result = await _serverQuery.GetServerPlayersAsync();
            return result;
        }
        */
    }
}
