namespace GhostPanel.Core.Providers
{
    public interface IPortAndIpProvider
    {
        int GetNextAvailablePort(int targetPort, string targetIp, int portIncrement);
    }
}