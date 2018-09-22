using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Packets.Source
{
    public class RequestPacket : IRequestPacket
    {
        public byte[] Header { get; set; }
    }
}
