using System.Collections.Generic;
using System.Reflection;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.GameServerUtils
{
    public static class ConfigFileUtils
    {

        /// <summary>
        /// Take a config template and a dict of replacement values.  Insert the values into the config template
        /// </summary>
        /// <param name="values">Dict of tokens and values</param>
        /// <param name="config">Config template</param>
        /// <returns>config template</returns>
        public static string InterpolateConfigFromDict(Dictionary<string, string> values, string config)
        {
            foreach (KeyValuePair<string, string> entry in values)
            {
                config = config.Replace(entry.Key, entry.Value);
            }

            return config;
        }

        /// <summary>
        /// Take a game server and convert it's properties to config variables
        /// </summary>
        /// <param name="gameServer">Game Server</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetVariablesFromGameServer(GameServer gameServer)
        {
            string key;
            string value;
            var variables = new Dictionary<string, string>();
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
                        variables.Add(string.Format("![{0}]", key), value);
                    }

                    continue;
                }

                key = prop.Name.ToString();
                value = prop.GetValue(gameServer).ToString();
                variables.Add(string.Format("![{0}]", key), value);

            }

            return variables;
        }
    }
}
