using GhostPanel.Rcon.Extensions;

namespace GhostPanel.Rcon.Steam.Packets
{
    /// <summary>
    /// Taken from https://github.com/ScottKaye/CoreRCON/blob/master/src/CoreRCON/Extensions.cs
    /// </summary>
    public class A2SPlayerResponsePacket : IQueryResponsePacket
    {
        public float Duration { get; private set; }
        public string Name { get; private set; }
        public short Score { get; private set; }

        public static A2SPlayerResponsePacket[] FromBytes(byte[] buffer)
        {
            int i = 7;

            A2SPlayerResponsePacket[] players = new A2SPlayerResponsePacket[buffer[5]];

            for (int p = 0; p < players.Length; ++p)
            {
                players[p] = new A2SPlayerResponsePacket
                {
                    Name = buffer.ReadNullTerminatedString(i, ref i),
                    Score = buffer.ReadShort(i, ref i),
                    Duration = buffer.ReadFloat(i + 2, ref i)
                };

                i += 3;
            }

            return players;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Score: {Score}, Duration: {Duration}";
        }
    }
}
