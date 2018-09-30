using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Commands
{
    public class CommandResponseGameServer : CommandResponseBase
    {
        public GameServer payload { get; set; }
    }
}
