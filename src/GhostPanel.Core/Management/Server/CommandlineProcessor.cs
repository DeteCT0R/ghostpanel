using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

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

            foreach (PropertyInfo prop in gameServer.GetType().GetProperties())
            {
                if (prop.GetValue(gameServer) == null)
                {
                    continue;
                }
                var propString = string.Format("{{{0}}}", prop.Name.ToString());
                outputCommandline = outputCommandline.Replace(propString, prop.GetValue(gameServer).ToString());

            }

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

        public string InterpolateFullCommandline(GameServer gameServer)
        {
            throw new NotImplementedException();
        }
    }
    
}
