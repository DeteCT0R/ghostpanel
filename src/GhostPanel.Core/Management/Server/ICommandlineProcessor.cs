using GhostPanel.Core.Data.Model;
using System.Collections.Generic;

namespace GhostPanel.Core.Management.Server
{
    public interface ICommandlineProcessor
    {
        string InterpolateCommandline(GameServer gameServer);
        string InterpolateCustomCommandline(string existingArgs, Dictionary<string,string> customArgs);
        string InterpolateFullCommandline(GameServer gameServer);
    }
}