using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Source.Packets
{
    class QueryResponseMultiPacket : QueryResponsePacket
    {
        public long Id { get; set; }
        public byte Total { get; set; }
        public byte PacketNumber { get; set; }
        public short Size { get; set; }
        public long Crc32Sum { get; set; }
    }
}
