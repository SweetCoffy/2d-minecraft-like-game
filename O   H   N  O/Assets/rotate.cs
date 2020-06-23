using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    entity e;
    public int itemNeeded = 23;
    // Start is called before the first frame update
    void Start()
    {
        e = GameObject.Find("Player").GetComponent<entity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        if(e.getSelectedItem() < e.storedItems.Count - 1) {
            if(e.storedItems[e.getSelectedItem()].id == itemNeeded) {
                transform.Rotate(0, 0, 90);
            }
        }
    }
}
