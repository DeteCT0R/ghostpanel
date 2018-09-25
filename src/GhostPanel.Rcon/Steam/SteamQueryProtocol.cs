using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CoreRCON;
using CoreRCON.PacketFormats;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Rcon.Steam
{
    public class SteamQueryProtocol : GameQuery
    {
        private IPEndPoint _endpoint;
        private readonly ILogger _logger; 

        public SteamQueryProtocol(IPEndPoint endpoint, ILoggerFactory logger)
        {
            _endpoint = endpoint;
            _logger = logger.CreateLogger<SteamQueryProtocol>();
        }

        public SteamQueryProtocol(IPAddress ipAddress, int port, ILoggerFactory logger) : this(new IPEndPoint(ipAddress, port), logger) { }

        public override async Task<ServerInfoBase> GetServerInfoAsync()
        {
            ServerInfoBase result;
            try
            {
                var steamResult = await ServerQuery.Info(_endpoint, ServerQuery.ServerType.Source) as SourceQueryInfo;
                result = ConvertInfoResponse(steamResult);
            }
            catch (SocketException e)
            {
                _logger.LogError("Socket Exception while trying to query server {ip}:{port}", _endpoint.Address, _endpoint.Port);
                result = new SteamServerInfo()
                {
                    MaxPlayers = 0,
                    CurrentPlayers = 0
                };
            }
            
            return result;
        }


        public override async Task<ServerPlayersBase[]> GetServerPlayersAsync()
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
            _logger.LogDebug("Converting SourceQueryInfo to SteamServerInfo");
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
