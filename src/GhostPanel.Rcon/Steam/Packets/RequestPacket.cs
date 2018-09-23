using System;

namespace GhostPanel.Rcon.Steam.Packets
{
    public class RequestPacket : IRequestPacket
    {
        public byte[] Header { get; set; }

        public byte[] ToBytes()
        {
            throw new NotImplementedException();
        }
    }
}
