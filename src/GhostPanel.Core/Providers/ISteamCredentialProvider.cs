namespace GhostPanel.Core.Providers
{
    public interface ISteamCredentialProvider
    {
        string GetUsername();
        string GetPassword();
        string GetCredentialString();
    }
}
