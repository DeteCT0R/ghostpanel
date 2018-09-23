namespace GhostPanel.Rcon.Steam.Packets
{
    public class QueryResponsePacket : IQueryResponsePacket
    {
        public long header { get; set; }
        public string payload { get; set; }

    }
}
