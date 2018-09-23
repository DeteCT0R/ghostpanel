using GhostPanel.Rcon.Extensions;

namespace GhostPanel.Rcon.Steam.Packets
{
    /// <summary>
    /// Taken from https://github.com/ScottKaye/CoreRCON/blob/master/src/CoreRCON/Extensions.cs
    /// </summary>
    public class As2InfoResponsePacket : QueryResponsePacket, IQueryResponsePacket
    {
        public byte Bots { get; private set; }
        public ServerEnvironment Environment { get; private set; }
        public  string Folder { get; private set; }
        public string Game { get; private set; }
        public short GameId { get; private set; }
        public string Map { get; private set; }
        public byte MaxPlayers { get; private set; }
        public string Name { get; private set; }
        public byte Players { get; private set; }
        public byte ProtocolVersion { get; private set; }
        public ServerType Type { get; private set; }
        public ServerVAC VAC { get; private set; }
        public ServerVisibility Visibility { get; private set; }

        public static As2InfoResponsePacket FromBytes(byte[] buffer)
        {
            int index = 6;
            return new As2InfoResponsePacket
            {
                ProtocolVersion = buffer[4],
                Name = buffer.ReadNullTerminatedString(index, ref index),
                Map = buffer.ReadNullTerminatedString(index, ref index),
                Folder = buffer.ReadNullTerminatedString(index, ref index),
                Game = buffer.ReadNullTerminatedString(index, ref index),
                GameId = buffer.ReadShort(index, ref index),
                Players = buffer[index++],
                MaxPlayers = buffer[index++],
                Bots = buffer[index++],
                Type = (ServerType)buffer[index++],
                Environment = (ServerEnvironment)buffer[index++],
                Visibility = (ServerVisibility)buffer[index++],
                VAC = (ServerVAC)buffer[index++]
            };
        }
    }
}
