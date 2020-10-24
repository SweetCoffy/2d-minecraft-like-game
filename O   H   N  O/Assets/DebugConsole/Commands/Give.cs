using UnityEngine;
namespace GameThing.DebugConsole.Commands {
    [CreateAssetMenu(fileName = "Give", menuName = "Commands/Give")]
    public class Give : ConsoleCommand {
        public override CommandResponse Execute(string[] args) {
            int id = int.Parse(args[0]);
            int amount = int.Parse(args[1]);
            if (!ItemData.IsValid(id)) return new CommandResponse("error", $"Item of ID {id} is not valid");
            droppedItem it = Instantiate(itemManager.main.droppedItemPrefab, Camera.main.transform.position, Quaternion.identity).GetComponent<droppedItem>();
            it.itemId = id;
            it.itemAmount = amount;
            return new CommandResponse("ok", $"Spawned {amount}x {ItemData.GetItemName(id)}");
        }
    }
}