using MediatR;

namespace GhostPanel.Core.Notifications
{
    public class ServerInstallStatusNotification : INotification
    {
        public string status;
        public string message;

        public ServerInstallStatusNotification()
        {
        }

        public ServerInstallStatusNotification(string status, string message)
        {
            this.status = status;
            this.message = message;
        }
    }
}
