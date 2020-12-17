using UnityEngine;
namespace GameThing.DebugConsole.Commands
{
    [CreateAssetMenu(fileName = "SetBG", menuName = "Commands/SetBG")]
    public class SetBG : ConsoleCommand
    {
        public override CommandResponse Execute(string[] args)
        {
            int i = int.Parse(args[0]);
            DaylightThing.m.currBackground = i;
            return new CommandResponse("ok", $"Set current background to {i}");
        }
    }
}