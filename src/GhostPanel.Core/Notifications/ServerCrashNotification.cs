using MediatR;

namespace GhostPanel.Core.Notifications
{
    public class ServerCrashNotification : INotification
    {
        public string message;

        public ServerCrashNotification(string message)
        {
            this.message = message;
        }
    }
}
