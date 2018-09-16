using Microsoft.Extensions.Logging;
using System.IO;

namespace GhostPanel.Core.Management.GameFiles
{
    public class GameFilesBase
    {
        private readonly ILogger _logger;
        private readonly string _installDir;

        public GameFilesBase(ILoggerFactory logger, string installDir)
        {
            _logger = logger.CreateLogger<GameFilesBase>();
            _installDir = installDir;
        }

        public void DeleteGameServerFiles()
        {
            _logger.LogInformation("Deleting game server files in {path}", _installDir);
            try
            {
                Directory.Delete(_installDir, true);
            }
            catch (IOException)
            {
                _logger.LogError("Failed to delete game server files in {path}", _installDir);
            }
        }

    }
}
