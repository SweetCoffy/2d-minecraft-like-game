namespace GameThing.DebugConsole.Commands
{
    public class CommandResponse
    {
        public string code = "";
        public string message = "";
        public CommandResponse(string code, string msg)
        {
            this.code = code;
            this.message = msg;
        }
        public bool IsCode(string code)
        {
            return this.code.Equals(code, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}