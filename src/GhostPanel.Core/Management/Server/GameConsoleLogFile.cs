using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GhostPanel.Core.Management.Server
{
    class GameConsoleLogFile : IGameConsole
    {
        private readonly string _logFile;
        private readonly ILogger _logger;
        private readonly int returnLines = 5;

        public GameConsoleLogFile(string logFile, ILoggerFactory logger)
        {
            _logFile = logFile;
            _logger = logger.CreateLogger<GameConsoleLogFile>();
        }

        public List<string> GetConsoleOutput()
        {
            List<string> lines = new List<string>();

            if (!File.Exists(_logFile))
            {
                _logger.LogError("Like file doesn't exist: {file}", _logFile);
                return lines;
            }

            lines = File.ReadAllLines(_logFile).ToList();
            return lines;


        }

        public string InputToConsole()
        {
            throw new NotImplementedException();
        }
    }
}
