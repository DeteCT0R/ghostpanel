using System;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Notifications;
using MediatR;

namespace GhostPanel.Core.Handlers.Notifications
{
    public class ServerCrashNotificationHandler : INotificationHandler<ServerCrashNotification>
    {
        public Task Handle(ServerCrashNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"----> Server crash notification: {notification.message}");
            return Task.CompletedTask;
        }
    }
}
