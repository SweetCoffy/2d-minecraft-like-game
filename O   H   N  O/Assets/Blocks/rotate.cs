using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    entity e;
    public int itemNeeded = 23;
    float h = 0;
    void Start()
    {
        e = GameObject.Find("Player").GetComponent<entity>();
    }
    void Update()
    {
        if (h > 0) h -= Time.deltaTime;
    }
    void OnMouseDown() {
        if(e.getSelectedItem() < e.storedItems.Count - 1) {
            if(e.storedItems[e.getSelectedItem()].id == itemNeeded && h <= 0) {
                transform.Rotate(0, 0, 90);
                h = 0.3f;
            }
        }
    }
}
