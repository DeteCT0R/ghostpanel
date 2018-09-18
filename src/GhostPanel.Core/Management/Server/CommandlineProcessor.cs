using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GhostPanel.Core.Management.Server
{
    public class CommandlineProcessor : ICommandlineProcessor
    {
        private readonly ILogger _logger;

        public CommandlineProcessor(ILogger<CommandlineProcessor> logger)
        {
            _logger = logger;
        }

        public string InterpolateCommandline(GameServer gameServer)
        {
            string outputCommandline = gameServer.CommandLine;

            outputCommandline = outputCommandline
                                .Replace("{ipAddress}", gameServer.IpAddress)
                                .Replace("{gamePort}", gameServer.GamePort.ToString());

            return outputCommandline;
        }

        public string InterpolateCustomCommandline(string args, Dictionary<string, string> customArgs)
        {
            
            foreach (KeyValuePair<string, string> arg in customArgs)
            {
                args += arg.Key;
                if (arg.Value != "")
                {
                    args = args + " " + arg.Value;
                }
            }

            return args;
        }
    }
    
}
