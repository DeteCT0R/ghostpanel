namespace GhostPanel.Core.Providers
{
    public interface IPortAndIpProvider
    {
        int GetNextAvailablePort(int gameId, string targetIp);
    }
}