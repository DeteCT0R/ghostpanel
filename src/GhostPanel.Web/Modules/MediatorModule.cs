using System.Reflection;
using Autofac;
using GhostPanel.Communication.Query;
using GhostPanel.Core.Commands;
using GhostPanel.Core.Handlers.Commands;
using GhostPanel.Core.Handlers.Notifications;
using MediatR;

namespace GhostPanel.Web.Modules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(CreateServerCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(RestartServerCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));



            // Notifications

            builder.RegisterAssemblyTypes(typeof(ServerInstallStatusNotificationHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));
            builder.RegisterAssemblyTypes(typeof(ServerStatsUpdateNotificationHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));


            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).
                As(typeof(IPipelineBehavior<,>));
        }
    }
}
