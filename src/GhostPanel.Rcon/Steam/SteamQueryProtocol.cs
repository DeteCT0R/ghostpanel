using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CoreRCON;
using CoreRCON.PacketFormats;

namespace GhostPanel.Rcon.Steam
{
    public class SteamQueryProtocol : IQueryProtocol
    {
        private IPEndPoint _endpoint;

        public SteamQueryProtocol(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        }

        public SteamQueryProtocol(IPAddress ipAddress, int port) : this(new IPEndPoint(ipAddress, port)) { }

        public async Task<ServerInfoBase> GetServerInfoAsync()
        {
            var result = await ServerQuery.Info(_endpoint, ServerQuery.ServerType.Source) as SourceQueryInfo;
            return ConvertInfoResponse(result);
        }


        public async Task<ServerPlayersBase[]> GetServerPlayersAsync()
        {
            var result = await ServerQuery.Players(_endpoint);
            return ConvertPlayerResponse(result);
        }

        /// <summary>
        /// Convert a SourceQueryInfo object from CoreRcon to a SteamServerInfo object
        /// </summary>
        /// <param name="info">SourceQueryInfo</param>
        /// <returns>SteamServerInfo</returns>
        private SteamServerInfo ConvertInfoResponse(SourceQueryInfo info)
        {
            return new SteamServerInfo()
            {
                Game = info.Game,
                Map = info.Map,
                MaxPlayers = info.MaxPlayers,
                CurrentPlayers = info.Players,
                Bots = info.Bots,
                Environment = info.Environment,
                Folder = info.Folder,
                GameId = info.GameId,
                Name = info.Name,
                ProtocolVersion = info.ProtocolVersion,
                Type = info.Type,
                VAC = info.VAC,
                Visibility = info.Visibility

            };
        }

        /// <summary>
        /// Take an of SeverQueryPlayer from CoreRcon and convert to SteamServerPlayer[]
        /// </summary>
        /// <param name="players">ServerQueryPlayer[]</param>
        /// <returns></returns>
        private SteamServerPlayer[] ConvertPlayerResponse(ServerQueryPlayer[] players)
        {
            List<SteamServerPlayer> final = new List<SteamServerPlayer>();
            foreach (ServerQueryPlayer player in players)
            {
                final.Add(new SteamServerPlayer()
                {
                    Name = player.Name,
                    Duration = player.Duration,
                    Score = player.Score
                });
            }

            return final.ToArray();
        }
    }
}
