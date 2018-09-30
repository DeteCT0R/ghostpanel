using GhostPanel.Core.Data.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GhostPanel.Core.Commands
{
    public class CommandResponseBase
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CommandResponseStatusEnum status { get; set; }
        public string message { get; set; }

    }
}
