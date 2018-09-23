using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Source.Packets
{
    public class QueryResponsePacket : IQueryResponsePacket
    {
        public long header { get; set; }
        public string payload { get; set; }

    }
}
