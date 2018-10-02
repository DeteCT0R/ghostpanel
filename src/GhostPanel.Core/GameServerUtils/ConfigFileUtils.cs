using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GhostPanel.Core.Data.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GhostPanel.Core.GameServerUtils
{
    public static class ConfigFileUtils
    {
        public static string InterpolateConfigFromGameServer(GameServer gameServer)
        {
            var replacements = new Dictionary<string, string>();
            string key;
            string value;
            string finalConfig = gameServer.Game.GameDefaultConfigFile.Template;
            foreach (PropertyInfo prop in gameServer.GetType().GetProperties())
            {
                
                var propValue = prop.GetValue(gameServer);
                if (propValue == null) continue;
                if (propValue is List<CustomVariable>)
                {
                    foreach (var custVar in propValue as List<CustomVariable>)
                    {
                        key = custVar.GetType().GetProperty("VariableName").GetValue(custVar).ToString();
                        value = custVar.GetType().GetProperty("VariableValue").GetValue(custVar).ToString();
                        replacements.Add(key, value);
                    }

                    continue;
                }

                key = prop.Name.ToString();
                value = prop.GetValue(gameServer).ToString();
                replacements.Add(key, value);

            }

            foreach (KeyValuePair<string, string> entry in replacements)
            {
                var replaceToken = string.Format("![{0}]", entry.Key);
                finalConfig = finalConfig.Replace(replaceToken, entry.Value);
            }

            return finalConfig;
        }
    }
}
