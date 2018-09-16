using GhostPanel.Core.Background;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GhostPanel.Web.Background
{
    public class ServerStatusBackgroundWorker : IHostedService
    {
        private readonly BackgroundManager _backgroundManager;

        public ServerStatusBackgroundWorker(BackgroundManager backgroundManager)
        {
            _backgroundManager = backgroundManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _backgroundManager.Run();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _backgroundManager.Stop();
        }
    }
}
