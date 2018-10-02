using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Communication.Query.Steam
{
    class SteamServerPlayer : ServerPlayersBase
    {
        public float Duration { get; set; }
        public int Score { get; set; }
    }
}
