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
                        if (gameServer.Pid == null)
                        {
                            if (gameServer.Status != ServerStatusStates.Stopped)
                            {
                                _logger.LogDebug("Game server {id} has no PID and state is not stopped.  Setting state to stopped", gameServer.Id);
                                gameServer.Status = ServerStatusStates.Stopped;
                                _repository.Update(gameServer);
                            }

                            continue;
                        }

                        if (_procManager.IsRunning(gameServer.Pid))
                        {
                            if (gameServer.Status != ServerStatusStates.Running)
                            {
                                _logger.LogDebug("Game server {id} is running but status doesn't match.  Setting status to running", gameServer.Id);
                                gameServer.RestartAttempts = 0;
                                gameServer.Status = ServerStatusStates.Running;
                                _repository.Update(gameServer);
                            }

                            continue;
                        }

                        // If we get here there's a PID but server is not running
                        HandleCrashedServer(gameServer);
                    }


                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            });

            await mainLoop;
        }

        private void HandleCrashedServer(GameServer gameServer)
        {
            if (gameServer.Status != ServerStatusStates.Crashed)
            {
                _logger.LogDebug("Game server {id} has a PID set but is not running.  Marking as crashed", gameServer.Id);
                gameServer.Status = ServerStatusStates.Crashed;
                _repository.Update(gameServer);

            }

            if (gameServer.RestartAttempts < 3) // TODO: Move max restarts to config
            {
                gameServer.RestartAttempts++;
                _logger.LogDebug("Attempt #{attempt} to restart server {id}", gameServer.RestartAttempts, gameServer.Id);
                _procManager.StartServer(gameServer);
                _repository.Update(gameServer);
            }
            else
            {
                _logger.LogDebug("Server {id} has hit the max restart attempts.  Stopping server", gameServer.Id);
                gameServer.Status = ServerStatusStates.Stopped;
                gameServer.Pid = null;
                _procManager.StopServer(gameServer);
                _repository.Update(gameServer);
            }
        }
    }
}
