using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GhostPanel.Web.Background
{
    public class PanelBackgroundWorker : IHostedService
    {
        private readonly BackgroundManager _backgroundManager;

        public PanelBackgroundWorker(BackgroundManager backgroundManager)
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
