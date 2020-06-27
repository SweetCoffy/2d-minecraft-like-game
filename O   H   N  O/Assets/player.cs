using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(entity))]

public class player : MonoBehaviour
{
    entity e;

    
    

    
    void Start()
    {
        e = GetComponent<entity>();
    }

    // Update is called once per frame
    void Update()
    {
            e.movementHorizontal(Input.GetAxis("Horizontal") * e.movementSpeed);
        
        if(Input.GetAxis("Mouse ScrollWheel") > 0) {
            e.setSelectedItem(e.getSelectedItem() - 1);
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0) {
            e.setSelectedItem(e.getSelectedItem() + 1);
        }
        
        if(Input.GetAxis("Jump") > 0) {
            e.jump(e.jumpForce * Input.GetAxis("Jump"));
        }   
        if(Input.GetAxis("Fire1") > 0) {
            e.useItem(e.getSelectedItem());
        }
    
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        e.placeBlockPosition = pz;

        GameObject.Find("BPD").transform.position = e.placeBlockPosition + new Vector3(0, 0, -9);

        if(Input.GetKeyDown(KeyCode.Q)) {
            e.dropItem(e.getSelectedItem(), false);
        }
        
        
        if(e.getSelectedItem() > e.storedItems.Count-1) {
            e.setSelectedItem(e.storedItems.Count - 1);
            return;
        }
        if(Input.GetMouseButtonDown(1)) {
            e.startBlockPlace(Resources.Load<GameObject>("Prefabs/block-" + e.storedItems[e.getSelectedItem()].id), (int)Mathf.Round(pz.x), (int)Mathf.Round(pz.y));
            e.consumeItem(e.getSelectedItem());
        }


    
    }
}
