using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Rcon.Packets.Source
{
    public class As2InfoResponsePacket : QueryResponsePacket, IQueryResponsePacket
    {
        public IQueryResponsePacket FromBytes(byte[] buffer)
        {
            return null;
        }
    }
}
