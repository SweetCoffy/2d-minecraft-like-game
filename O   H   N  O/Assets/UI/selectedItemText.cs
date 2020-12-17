using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectedItemText : MonoBehaviour
{
    entity e;
    itemManager im;
    // Start is called before the first frame update
    void Start()
    {
       e = GameObject.Find("Player").GetComponent<entity>();
       im = GameObject.Find("ItemManager").GetComponent<itemManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(e.getSelectedItem() > e.storedItems.Count - 1) {
            GetComponent<Text>().text = "0x Nothing";
            return;
        }
        GetComponent<Text>().text = $"{e.storedItems[e.getSelectedItem()].amount}x {im.itemNames[e.storedItems[e.getSelectedItem()].id]}";
    }
}
