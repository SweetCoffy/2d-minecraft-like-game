using UnityEngine;
using GameThing.DebugConsole.UI;
namespace GameThing.DebugConsole.Commands {
    [CreateAssetMenu(fileName = "Itemlist", menuName = "Commands/Itemlist")]
    public class Itemlist : ConsoleCommand {
        public override CommandResponse Execute(string[] args) {
            string[] items = ItemManager.main.itemNames;
            ConsoleMessageContainer.m.Add("------------------------------------");
            for (int i = 0; i < items.Length; i++) {
                ConsoleMessageContainer.m.Add($"ID#<color=gray>{i}</color>: {items[i]}");
            }
            return new CommandResponse("ok", "------------------------------------");
        }
    }
}