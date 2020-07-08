using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    public Sprite[] itemTextures;
    public Sprite unknownTexture;
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
