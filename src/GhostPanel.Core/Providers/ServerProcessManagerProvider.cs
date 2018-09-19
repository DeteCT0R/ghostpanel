using GhostPanel.Core.Exceptions;
using GhostPanel.Core.Management;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostPanel.Core.Providers
{
    public class ServerProcessManagerProvider : IServerProcessManagerProvider
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _logFactory;
        private readonly IList<IServerProcessManager> _procManagers;

        public ServerProcessManagerProvider(ILoggerFactory logFactory, IList<IServerProcessManager> fileManager)
        {
            _logger = logFactory.CreateLogger<ServerProcessManagerProvider>();
            _logFactory = logFactory;
            _procManagers = fileManager;
        }

        public IServerProcessManager GetProcessManagerProvider()
        {
            // TODO: Detect operating system
            var provider = _procManagers.OfType<ServerProcessManagerWin>().FirstOrDefault();
            if (provider == null)
            {
                _logger.LogError("Unable to find Process Manager for Type ServerProcessManagerWin");
                throw new FailedToLocateProcessManager("Unable to locate Server Process Manager For Windows");
            }

            return provider;
        }

    }
}
