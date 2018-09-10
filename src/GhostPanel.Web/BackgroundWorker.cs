using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public class BackgroundWorker : IHostedService
    {
        private readonly BackgroundManager _backgroundManager;

        public BackgroundWorker(BackgroundManager backgroundManager)
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
