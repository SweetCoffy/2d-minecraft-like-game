using UnityEngine;
namespace GameThing.DebugConsole.Commands {
    [CreateAssetMenu(fileName = "Give2", menuName = "Commands/Give2")]
    public class Give2 : ConsoleCommand {
        public override CommandResponse Execute(string[] args) {
            int id = int.Parse(args[0]);
            if (!ItemData.IsValid(id)) return new CommandResponse("error", $"Item of ID {id} is not valid");
            DroppedItem it = Instantiate(ItemManager.main.droppedItemPrefab, Camera.main.transform.position, Quaternion.identity).GetComponent<DroppedItem>();
            it.itemId = id;
            it.itemAmount = 1;
            return new CommandResponse("ok", $"Spawned {ItemData.GetItemName(id)}");
        }
    }
}