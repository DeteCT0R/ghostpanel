using System;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Commands;
using GhostPanel.Core.Data;
using MediatR;

namespace GhostPanel.Core.Mediator.Behaviors
{
    public class BeforeStartedBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : StartServerCommand
    {
        private readonly IRepository _repository;
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Console.WriteLine("---> Before server start");
            return await next();
        }
    }
}
