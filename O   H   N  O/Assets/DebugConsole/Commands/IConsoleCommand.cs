namespace GameThing.DebugConsole.Commands
{
    public interface IConsoleCommand
    {
        string CommandName { get; }
        CommandResponse Execute(string[] args);
    }
}