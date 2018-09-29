using System;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using MediatR;

namespace GhostPanel.Core.Commands
{
    public class RestartServerCommandHandler : IRequestHandler<RestartServerCommand, CommandResponse>
    {
        private readonly IMediator _mediator;
        private readonly IServerProcessManager _procManager;

        public RestartServerCommandHandler(IMediator mediator, IServerProcessManagerProvider procProvider)
        {
            _mediator = mediator;
            _procManager = procProvider.GetProcessManagerProvider();
        }

        public Task<CommandResponse> Handle(RestartServerCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            try
            {
                _procManager.RestartServer(request.gameServer);
                response.status = "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.status = "Error";
                response.payload = e.ToString();
            }

            return Task.FromResult(response);
        }

    }
}
