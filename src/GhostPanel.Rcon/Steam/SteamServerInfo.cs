using CoreRCON.PacketFormats;

namespace GhostPanel.Rcon.Steam
{
    public class SteamServerInfo : ServerInfoBase
    {
        public ServerEnvironment Environment { get; set; }
        public string Folder { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public byte Bots { get; set; }
        public int ProtocolVersion { get; set; }
        public ServerType Type { get; set; }
        public ServerVAC VAC { get; set; }
        public ServerVisibility Visibility { get; set; }
    }
}
