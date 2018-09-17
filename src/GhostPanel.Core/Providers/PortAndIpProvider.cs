using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Data;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Providers
{
    public class PortAndIpProvider
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public PortAndIpProvider(IRepository repository, ILogger<PortAndIpProvider> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public GetNextAvailablePort(int gameId)
        {

        }

    }
}
