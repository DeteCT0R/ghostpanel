using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Steam.Packets
{
    public class As2InfoRequestPacket : IRequestPacket
    {

        public byte[] ToBytes()
        {

            var header = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x54 };
            var payload = "Source Engine Query";

            List<byte> dataGram = new List<byte>();
            foreach (var b in header)
            {
                dataGram.Add(b);
            }

            foreach (var payloadByte in Encoding.UTF8.GetBytes(payload))
            {
                dataGram.Add(payloadByte);
            }

            dataGram.Add(0);


            return dataGram.ToArray();

        }
    }
}
