using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using GhostPanel.Core.GameServerUtils;

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
            var variables = ConfigFileUtils.GetVariablesFromGameServer(gameServer);
            foreach (KeyValuePair<string, string> kv in variables)
            {
                _logger.LogDebug($"Attempting to insert {kv.Key} into commandline with value {kv.Value}");
                outputCommandline = outputCommandline.Replace(kv.Key, kv.Value);
            }

            return outputCommandline;
        }

        public string InterpolateCustomCommandline(Dictionary<string, string> customArgs)
        {
            string args = "";
            foreach (KeyValuePair<string, string> arg in customArgs)
            {
                args += " " + arg.Key;
                if (arg.Value != "")
                {
                    args = args + " " + arg.Value;
                }
            }

            return args.TrimEnd().TrimStart();
        }

        public string InterpolateFullCommandline(GameServer gameServer)
        {
            throw new NotImplementedException();
        }
    }
    
}
