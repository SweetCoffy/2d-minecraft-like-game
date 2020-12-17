using UnityEngine;
namespace GameThing.DebugConsole.Commands
{
    public class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandName = string.Empty;
        public string CommandName => commandName;
        public virtual CommandResponse Execute(string[] args)
        {
            return new CommandResponse("error", "This comand's <color=gray>Execute</color> hasn't been implemented yet");
        }
    }
}