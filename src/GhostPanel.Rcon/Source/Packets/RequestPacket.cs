using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Source.Packets
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
