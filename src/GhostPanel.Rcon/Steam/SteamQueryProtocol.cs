using System.Linq;
using GhostPanel.Core.Data.Model;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using GhostPanel.Rcon.Steam.Packets;

namespace GhostPanel.Rcon.Steam
{
    public class SteamQueryProtocol : IQueryProtocol
    {

        private UdpClient _client { get; set; }
        private IPEndPoint _endpoint { get; set; }


        public SteamQueryProtocol(IPEndPoint endPoint)
        {
            _endpoint = endPoint;
            _client = new UdpClient();
        }

        public SteamQueryProtocol(IPAddress ipAddress, int queryPort)
        {
            _endpoint = new IPEndPoint(ipAddress, queryPort);
            _client = new UdpClient();
        }

        public SteamQueryProtocol(UdpClient client, IPEndPoint endpoint)
        {
            _client = client;
            _endpoint = endpoint;
        }

        public As2InfoResponsePacket GetServerInfo()
        {
            var endPoint = _endpoint;
            var infoPacket = new As2InfoRequestPacket().ToBytes();
            _client.Send(infoPacket, infoPacket.Length, endPoint);
            byte[] response = _client.Receive(ref endPoint);
            return As2InfoResponsePacket.FromBytes(response);
        }

        public async Task<As2InfoResponsePacket> GetServerInfoAsync()
        {

            var infoPacket = new As2InfoRequestPacket().ToBytes();
            await _client.SendAsync(infoPacket, infoPacket.Length, _endpoint);
            var response = await _client.ReceiveAsync();
            return As2InfoResponsePacket.FromBytes(response.Buffer);
        }

        public A2SPlayerResponsePacket[] GetServerPlayers()
        {
            var endPoint = _endpoint;
            var playerRequest = new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0x55}
                .Concat(GetChallange()).ToArray();
            _client.Send(playerRequest, playerRequest.Length, endPoint);
            var response = _client.Receive(ref endPoint);
            return A2SPlayerResponsePacket.FromBytes(response);
        }

        public async Task<A2SPlayerResponsePacket[]> GetServerPlayersAsync()
        {
            var challange = await GetChallangeAsync();
            var playerRequest = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55 }
                .Concat(challange).ToArray();
            await _client.SendAsync(playerRequest, playerRequest.Length, _endpoint);
            var response = await _client.ReceiveAsync();
            return A2SPlayerResponsePacket.FromBytes(response.Buffer);
        }

        private byte[] GetChallange()
        {
            var endPoint = _endpoint;
            var challangePacket = ChallangePacket.GetChallangePacket();
            _client.Send(challangePacket, challangePacket.Length, endPoint);
            return ChallangePacket.CleanChallangeResponse(_client.Receive(ref endPoint));
        }

        private async Task<byte[]> GetChallangeAsync()
        {
            var challangePacket = ChallangePacket.GetChallangePacket();
            await _client.SendAsync(challangePacket, challangePacket.Length, _endpoint);
            var response = await _client.ReceiveAsync();
            return ChallangePacket.CleanChallangeResponse(response.Buffer);
        }
    }
}
