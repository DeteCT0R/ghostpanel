using System.Threading.Tasks;

namespace GhostPanel.Core.Automation.StartProcess
{
    public interface IBeforeStartedAction
    {
        Task Invoke(int gameServerId);
    }
}