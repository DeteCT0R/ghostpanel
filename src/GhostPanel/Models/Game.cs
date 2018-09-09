using System;
using System.Collections.Generic;
using System.Text;

namespace GameHostDemo.Models
{
    class Game : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SteamAppId { get; set; }
        public string SteamUrl { get; set; }
        public string ExeName { get; set; }
        public int MaxSlots { get; set; }
        public int MinSlots { get; set; }
        public string DefaultPath { get; set; }
        public List<GameServer> GameServers { get; set; }

    }
}
