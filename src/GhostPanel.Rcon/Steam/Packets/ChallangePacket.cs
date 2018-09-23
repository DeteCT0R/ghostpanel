using System.Linq;

namespace GhostPanel.Rcon.Steam.Packets
{
    public static class ChallangePacket
    {
        public static byte[] GetChallangePacket()
        {
            return new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF};
        }

        public static byte[] CleanChallangeResponse(byte[] buffer)
        {
            return buffer.Skip(5).Take(4).ToArray();
        }
    }
}
