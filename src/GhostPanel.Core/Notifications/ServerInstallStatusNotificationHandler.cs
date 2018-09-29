using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace GhostPanel.Core.Notifications
{
    public class ServerInstallStatusNotificationHandler : INotificationHandler<ServerInstallStatusNotification>
    {
        public Task Handle(ServerInstallStatusNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"---> Install Status: {notification.status} - {notification.message}");
            return Task.CompletedTask;
        }
    }
}
