using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Automation.StartProcess;
using GhostPanel.Core.Commands;
using GhostPanel.Core.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Mediator.Behaviors
{
    public class ServerStartBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : StartServerCommand
    {
        private readonly IRepository _repository;
        private readonly ILogger<ServerStartBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IBeforeStartedAction> _beforeStartActions;

        public ServerStartBehavior(IRepository repository, ILogger<ServerStartBehavior<TRequest, TResponse>> logger, IEnumerable<IBeforeStartedAction> beforeStartActions)
        {
            _repository = repository;
            _beforeStartActions = beforeStartActions;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            foreach (var beforeStartedAction in _beforeStartActions)
            {
                await beforeStartedAction.Invoke(request.gameServerId);
            }
            return await next();
        }
    }
}
