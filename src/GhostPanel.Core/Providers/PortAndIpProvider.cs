using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Providers
{
    public class PortAndIpProvider : IPortAndIpProvider
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public PortAndIpProvider(IRepository repository, ILogger<PortAndIpProvider> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public int GetNextAvailablePort(int targetPort, string targetIp, int portIncrement = 10)
        {

            int testPort = targetPort;

            // TODO: This seems like a terriable idea
            while (true)
            {
                _logger.LogDebug("Testing port {port}");
                var result = _repository.List(GameServerPolicy.ByGamePort(testPort, targetIp));
                if (result.Count > 0)
                {
                    _logger.LogDebug("Found game server using port {port}, skipping", testPort);
                    testPort = testPort + portIncrement;
                    continue;
                }

                return testPort;
            }

            


        }

    }
}
