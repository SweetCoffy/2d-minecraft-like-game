using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemText : MonoBehaviour
{
    Entity e;
    ItemManager im;
    // Start is called before the first frame update
    void Start()
    {
       e = GameObject.Find("Player").GetComponent<Entity>();
       im = GameObject.Find("ItemManager").GetComponent<ItemManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(e.GetSelectedItem() > e.storedItems.Count - 1) {
            GetComponent<Text>().text = "0x Nothing";
            return;
        }
        GetComponent<Text>().text = $"{e.storedItems[e.GetSelectedItem()].amount}x {im.itemNames[e.storedItems[e.GetSelectedItem()].id]}";
    }
}
