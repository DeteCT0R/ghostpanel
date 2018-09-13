namespace GhostPanel.Core.Data
{
    class SteamCmd
    {
        public SteamCmd(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public string userName { get; set; }
        public string password { get; set; }
    }
}
