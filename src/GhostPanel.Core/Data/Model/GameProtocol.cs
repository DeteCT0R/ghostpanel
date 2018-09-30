using System.Collections.Generic;

namespace GhostPanel.Core.Data.Model
{
    public class GameProtocol : DataEntity
    {
        public string FullTypeName { get; set; }
        public string Name { get; set; }
        public string ServerInfoType { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
