namespace GhostPanel.Core.Data.Model
{
    public class CustomVariable : DataEntity
    {
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public GameServer GameServer { get; set; }
    }
}
