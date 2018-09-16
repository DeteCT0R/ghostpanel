using System;
using GhostPanel.Core.Management.GameFiles;
using GhostPanel.Core.Managment.GameFiles;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Management.GameFiles
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
