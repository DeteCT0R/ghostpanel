using System.Threading.Tasks;


namespace GhostPanel.Rcon
{
    public class GameQuery
    {
        private IQueryProtocol _serverQuery;

        public GameQuery(IQueryProtocol serverQuery)
        {
            _serverQuery = serverQuery;
        }

        public async Task<ServerInfoBase> GetServerInfo()
        {
            var result = await _serverQuery.GetServerInfoAsync();
            return result;
        }
    }
}
