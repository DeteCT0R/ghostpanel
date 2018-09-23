using System;
using System.Net;
using System.Threading.Tasks;
using GhostPanel.Core.Data.Model;
using GhostPanel.Rcon.Steam;

namespace GhostPanel.Rcon.Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            var task = Task.Run(async () =>
            {
                GameServer server1 = new GameServer()
                {
                    IpAddress = "64.42.183.170",
                    QueryPort = 2303
                };

                SteamQueryProtocol rcon1 = new SteamQueryProtocol(IPAddress.Parse("64.42.183.170"), 2303);

                var info = await rcon1.GetServerInfoAsync();
                var players = await rcon1.GetServerPlayersAsync();

                Console.WriteLine($"Server Name Is {info.Name} and has {players.Length} players");
            });
            task.GetAwaiter().GetResult();
            Console.ReadLine();

            /*
            SourceProtocol rcon = new SourceProtocol(server);

            var result = rcon.GetServerPlayers();

            Console.WriteLine();

            
            UdpClient client = new UdpClient();
            var endpoint = new IPEndPoint(IPAddress.Parse("192.168.1.153"), 27015);
            
            var result =
                client.Send(
                    new byte[]
                    {
                        0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69,
                        0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00
                    }, 25, endpoint);
            

            byte[] exampleQuery = new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69,
                0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00
            };

            As2InfoRequestPacket queryPacket = new As2InfoRequestPacket();
            var bytePacket = queryPacket.ToBytes();


            bool packetsMatch = exampleQuery.SequenceEqual(bytePacket);

            client.Send(bytePacket, bytePacket.Length, endpoint);
            byte[] response = client.Receive(ref endpoint);

            int startOffset = 6;
            int endOffset = Array.IndexOf(response, (byte) 0, startOffset);
            string title = Encoding.UTF8.GetString(response, startOffset, endOffset - startOffset);
            startOffset = endOffset + 1;
            endOffset = Array.IndexOf(response, (byte)0, startOffset);
            string map = Encoding.UTF8.GetString(response, startOffset, endOffset - startOffset);
            startOffset = endOffset + 1;
            endOffset = Array.IndexOf(response, (byte)0, startOffset);
            string folder = Encoding.UTF8.GetString(response, startOffset, endOffset - startOffset);
            startOffset = endOffset + 1;
            endOffset = Array.IndexOf(response, (byte)0, startOffset);
            string game = Encoding.UTF8.GetString(response, startOffset, endOffset - startOffset);
            */


        }
    }
}
