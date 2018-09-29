using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GhostPanel.Core.Commands
{
    public class StopServerCommandHandler : IRequestHandler<StopServerCommand, CommandResponse>
    {
        private readonly IMediator _mediator;
        private readonly IServerProcessManager _procManager;

        public StopServerCommandHandler(IMediator mediator, IServerProcessManagerProvider procProvider)
        {
            _mediator = mediator;
            _procManager = procProvider.GetProcessManagerProvider();
        }

        public Task<CommandResponse> Handle(StopServerCommand request, CancellationToken cancellationToken)
        {
            var response = new CommandResponse();
            try
            {
                _procManager.StopServer(request.gameServer);
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
