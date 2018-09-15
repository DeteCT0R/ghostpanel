using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Managment;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Management
{
    class GameFileManagerFactory : IGameFileManagerFactory
    {
        private readonly ILogger _logger;

        public GameFileManagerFactory(ILogger<GameFileManagerFactory> logger)
        {
            _logger = logger;
        }

        public IGameFileManager GetGameFileManager(string installDir, int appId)
        {
            throw new NotImplementedException();
        }
    }
}
