
using System;
using System.Collections.Generic;


namespace GhostPanel.Core.Data.Model
{
    public class GameServer : DataEntity
    {
        public int GameId { get; set; }
        public string IpAddress { get; set; }
        public int GamePort { get; set; }
        public int QueryPort { get; set; }
        public string ServerName { get; set; }
        public bool IsEnabled { get; set; }
        public string Version { get; set; }
        public string StartDirectory { get; set; }
        public string HomeDirectory { get; set; }
        public string CommandLine { get; set; }
        public int Slots { get; set; }
        public string RconPassword { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public Dictionary<string, string> CustomCommandLineArgs { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User CreatedBy { get; set; }
        public User ModifiedBy { get; set; }
        public Game Game { get; set; }
        public int OwnerId { get; set; }
        public GameServerCurrentStats GameServerCurrentStats { get; set; }
        public User Owner { get; set; }
        public ICollection<GameServerConfigFile> GameConfigFiles { get; set; }
        public ICollection<CustomVariable> CustomVariables { get; set; }
        public ICollection<ScheduledTask> ScheduledTasks { get; set; }

    }
}
