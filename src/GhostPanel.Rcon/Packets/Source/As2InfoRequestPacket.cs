using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Packets.Source
{
    public class As2InfoRequestPacket : RequestPacket
    {
        public As2InfoRequestPacket()
        {
            base.Header = new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0x54};
            Payload = "Source Engine Query";
        }

        public string Payload { get; set; }

        public byte[] ToBytes()
        {
            var payloadBytes = Encoding.UTF8.GetBytes(Payload);
            List<byte> final = new List<byte>();
            foreach (var b in Header)
            {
                final.Add(b);
            }

            foreach (var payloadByte in payloadBytes)
            {
                final.Add(payloadByte);
            }

            final.Add((byte)0);
            return final.ToArray();

        }
    }
}
