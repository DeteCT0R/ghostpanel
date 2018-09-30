using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Data.Model
{
    public class User : DataEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int GameServerId { get; set; }
        public ICollection<GameServer> GameServers { get; set; }
    }
}
