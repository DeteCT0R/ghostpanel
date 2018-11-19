using System;
using System.IO;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.GameServerUtils;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Automation.StartProcess
{
    public class ProcessConfigFilesBeforeStarted : IBeforeStartedAction
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public ProcessConfigFilesBeforeStarted(IRepository repository, ILogger<ProcessConfigFilesBeforeStarted> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Invoke(int gameServerId)
        {
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(gameServerId));
            _repository.List(GameServerConfigFilePolicy.ByServerId(gameServer.Id));
            foreach (var gameServerGameConfigFile in gameServer.GameConfigFiles)
            {
                _logger.LogDebug($"Processing config {gameServerGameConfigFile.FilePath} for game server {gameServer.Id}");
                var variables = ConfigFileUtils.GetVariablesFromGameServer(gameServer);
                if (File.Exists(Path.Combine(gameServer.HomeDirectory, gameServerGameConfigFile.FilePath)))
                {
                    _logger.LogDebug($"Found existing config ({gameServerGameConfigFile.FilePath})");
                    FileStream fileStream = new FileStream(Path.Combine(gameServer.HomeDirectory, gameServerGameConfigFile.FilePath), FileMode.Open);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        gameServerGameConfigFile.FileContent = reader.ReadToEnd();
                    }

                    _repository.Update(gameServerGameConfigFile);
                }

                var config = ConfigFileUtils.InterpolateConfigFromDict(variables, gameServerGameConfigFile.FileContent);
                try
                {
                    using (StreamWriter file =
                        new StreamWriter(Path.Combine(gameServer.HomeDirectory, gameServerGameConfigFile.FilePath)))
                    {
                        file.Write(config);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to write {gameServerGameConfigFile.FilePath} for server {gameServer.Id}");
                }

            }

        }
    }
}
