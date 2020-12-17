using GameThing.DebugConsole.Commands;
using System.Collections.Generic;
namespace GameThing.DebugConsole
{

    public class Console
    {
        private readonly string prefix;
        private readonly IEnumerable<IConsoleCommand> commands;
        public Console(string prefix, IEnumerable<IConsoleCommand> commands)
        {
            this.prefix = prefix;
            this.commands = commands;
        }
        public CommandResponse ExecuteCommand(string name)
        {
            if (!name.StartsWith(prefix)) return new CommandResponse("error", $"Invalid command, all commands must start with {prefix}");
            name = name.Remove(0, prefix.Length);
            string[] split = name.Split(' ');
            string _name = split[0];
            List<string> args = new List<string>(split);
            args.RemoveAt(0);
            try
            {
                return ExecuteCommand(_name, args.ToArray());
            }
            catch (System.Exception e)
            {
                return new CommandResponse("error", $"{e.GetType().ToString()}: {e.Message}");
            }
        }

        public CommandResponse ExecuteCommand(string name, string[] args)
        {
            foreach (IConsoleCommand command in commands)
            {
                if (!name.Equals(command.CommandName)) continue;
                CommandResponse response = command.Execute(args);
                if (response.code == "ok") return response;
            }
            return new CommandResponse("error", "Invalid command");
        }
    }
}