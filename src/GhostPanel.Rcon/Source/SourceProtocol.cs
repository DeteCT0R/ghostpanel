using System.Linq;
using GhostPanel.Core.Data.Model;
using System.Net;
using System.Net.Sockets;
using GhostPanel.Rcon.Source.Packets;

namespace GhostPanel.Rcon.Source
{
    public class SourceProtocol : IRconProtocol
    {

        private Socket _tcpConn { get; set; }
        private UdpClient _udpConn { get; set; }
        private IPEndPoint _endpoint { get; set; }

        public SourceProtocol(GameServer gameServer)
        {
            _endpoint = new IPEndPoint(IPAddress.Parse(gameServer.IpAddress), gameServer.QueryPort);
            _udpConn = new UdpClient();
            _tcpConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }


        public IQueryResponsePacket GetServerInfo()
        {
            var endPoint = _endpoint;
            var infoPacket = new As2InfoRequestPacket().ToBytes();
            _udpConn.Send(infoPacket, infoPacket.Length, endPoint);
            byte[] response = _udpConn.Receive(ref endPoint);
            return As2InfoResponsePacket.FromBytes(response);
        }

        public IQueryResponsePacket[] GetServerPlayers()
        {
            var endPoint = _endpoint;
            var playerRequest = new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0x55}
                .Concat(GetChallange()).ToArray();
            _udpConn.Send(playerRequest, playerRequest.Length, endPoint);
            var response = _udpConn.Receive(ref endPoint);
            return A2SPlayerResponsePacket.FromBytes(response);
        }

        public byte[] GetChallange()
        {
            var endPoint = _endpoint;
            var challangePacket = ChallangePacket.GetChallangePacket();
            _udpConn.Send(challangePacket, challangePacket.Length, endPoint);
            return ChallangePacket.CleanChallangeResponse(_udpConn.Receive(ref endPoint));
        }
    }
}
