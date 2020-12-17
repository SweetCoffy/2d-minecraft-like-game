using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    public Sprite[] itemTextures;
    public Sprite unknownTexture;
    public GameObject droppedItemPrefab;
    public Sprite blankTexture;
    public string[] itemNames;
    public static entity player;
    public static itemManager main;
    void Awake() {
        main = this;
        player = GameObject.Find("Player").GetComponent<entity>();
        InvokeRepeating("UpdateItem", 0, 15f);
    }


    void UpdateItem() {
        string t;
        if(player.getSelectedItem() < player.storedItems.Count - 1) {
            t = $"{player.storedItems[player.getSelectedItem()].amount}x {itemNames[player.storedItems[player.getSelectedItem()].id]}";
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
            if (id > itemManager.main.itemTextures.Length - 1) return itemManager.main.unknownTexture;
            if (id < 0) return itemManager.main.unknownTexture;
            return itemManager.main.itemTextures[id];
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
            if (id > itemManager.main.itemNames.Length - 1) return "oh no";
            if (id < 0) return "oh no";
            return itemManager.main.itemNames[id];
        }
    }
    public bool isValid {
        get {
            return id > 0 && id < itemManager.main.itemNames.Length;
        }
    }
    public ItemData(int id) {
        this.id = id;
    }
}
