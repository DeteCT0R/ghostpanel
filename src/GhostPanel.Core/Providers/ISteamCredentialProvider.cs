using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Core.Providers
{
    public interface ISteamCredentialProvider
    {
        string GetUsername();
        string GetPassword();
    }
}
