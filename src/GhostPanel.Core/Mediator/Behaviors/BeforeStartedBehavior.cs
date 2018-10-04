using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Commands;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.GameServerUtils;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Mediator.Behaviors
{
    public class BeforeStartedBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : StartServerCommand
    {
        private readonly IRepository _repository;
        private readonly ILogger<BeforeStartedBehavior<TRequest, TResponse>> _logger;

        public BeforeStartedBehavior(IRepository repository, ILogger<BeforeStartedBehavior<TRequest, TResponse>> logger)
        {
            _repository = repository;

        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(request.gameServerId));
            _repository.List(GameServerConfigFilePolicy.ByServerId(gameServer.Id));
            foreach (var gameServerGameConfigFile in gameServer.GameConfigFiles)
            {
                var variables = ConfigFileUtils.GetVariablesFromGameServer(gameServer);
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
            Console.WriteLine("---> Before server start");
            return await next();
        }
    }
}
