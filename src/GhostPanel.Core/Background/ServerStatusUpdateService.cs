using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Managment;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;

namespace GhostPanel.Core.Background
{
    public class ServerStatusUpdateService : IBackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServerProcessManager _procManager;
        private readonly IRepository _repository;

        public ServerStatusUpdateService(ILogger<ServerStatusUpdateService> logger, IRepository repository, IServerProcessManagerProvider procManager)
        {
            _logger = logger;
            _repository = repository;
            _procManager = procManager.GetProcessManagerProvider();
        }

        public void AddTask(IQueuedTask taskToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task Start()
        {
            _logger.LogDebug("Running Server Status Update Loop");
            var mainLoop = Task.Run(async () =>
            {
                while (true)
                {
                    var servers = _repository.List<GameServer>();
                    foreach (GameServer gameServer in servers)
                    {
                        if (gameServer.Pid == null && gameServer.Status != ServerStatusStates.Stopped)
                        {
                            _logger.LogDebug("Game server {id} has no PID and state is not stopped.  Setting state to stopped", gameServer.Id);
                            gameServer.Status = ServerStatusStates.Stopped;
                            _repository.Update(gameServer);
                            continue;
                        }

                        if (_procManager.IsRunning(gameServer.Pid))
                        {
                            if (gameServer.Status != ServerStatusStates.Running)
                            {
                                _logger.LogDebug("Game server {id} is running but status doesn't match.  Setting status to running", gameServer.Id);
                                gameServer.Status = ServerStatusStates.Running;
                                _repository.Update(gameServer);
                                continue;

                            }
                            
                        }
                        else
                        {
                            if (gameServer.Status != ServerStatusStates.Crashed)
                            {
                                _logger.LogDebug("Game server {id} has a PID set but is not running.  Marking as crashed", gameServer.Id);
                                gameServer.Status = ServerStatusStates.Crashed;
                                _repository.Update(gameServer);
                                continue;
                            }
                        }

                    }
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            });

            await mainLoop;
        }
    }
}
