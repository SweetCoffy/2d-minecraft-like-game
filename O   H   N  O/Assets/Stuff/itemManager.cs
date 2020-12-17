using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Sprite[] itemTextures;
    public Sprite unknownTexture;
    public GameObject droppedItemPrefab;
    public Sprite blankTexture;
    public string[] itemNames;
    public static Entity player;
    public static ItemManager main;
    void Awake() {
        main = this;
        player = GameObject.Find("Player").GetComponent<Entity>();
        InvokeRepeating("UpdateItem", 0, 15f);
    }


    void UpdateItem() {
        string t;
        if(player.GetSelectedItem() < player.storedItems.Count - 1) {
            t = $"{player.storedItems[player.GetSelectedItem()].amount}x {itemNames[player.storedItems[player.GetSelectedItem()].id]}";
        } else {
            t = "Nothing";
        }
        
        
        DiscordController.main.UpdateHeldItem(t);
    }


}

[System.Serializable]
public class ItemData {
    public Sprite texture {
        get {
            if (id > ItemManager.main.itemTextures.Length - 1) return ItemManager.main.unknownTexture;
            if (id < 0) return ItemManager.main.unknownTexture;
            return ItemManager.main.itemTextures[id];
        }
    }
    public static string GetItemName(int id) {
        return new ItemData(id).name;
    }
    public static ItemData GetItem(int id) {
        return new ItemData(id);
    }
    public static bool IsValid(int id) {
        return new ItemData(id).isValid;
    }
    public int id;
    public string name {
        get {
            if (id > ItemManager.main.itemNames.Length - 1) return "oh no";
            if (id < 0) return "oh no";
            return ItemManager.main.itemNames[id];
        }
    }
    public bool isValid {
        get {
            return id > 0 && id < ItemManager.main.itemNames.Length;
        }
    }
    public ItemData(int id) {
        this.id = id;
    }
}
