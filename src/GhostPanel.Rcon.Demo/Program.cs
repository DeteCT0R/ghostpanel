using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using GhostPanel.Communication.Query.Steam;
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
                UdpClient client = new UdpClient();
                var steam = new SteamQueryProtocol(new IPEndPoint(IPAddress.Parse("192.168.1.153"), 27015), null);
                var result = await steam.GetServerInfoAsync();

                Console.WriteLine("");
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
