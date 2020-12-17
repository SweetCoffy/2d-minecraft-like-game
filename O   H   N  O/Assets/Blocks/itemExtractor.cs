using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExtractor : MonoBehaviour
{
    public Vector3 offset;
    public int itemId;
    public int itemAmount;
    public float waitTime = 1;
    float progress;
    public GameObject drillEffect;
    public float getProgressNormalized() {
        return progress/waitTime;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(progress < waitTime) {
            progress += Time.deltaTime;
        }
        if(progress >= waitTime) {
            drill();
        }
    }

    public void drill() {
        progress = 0;
        GameObject spawned = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position + offset, transform.rotation);
        DroppedItem i = spawned.GetComponent<DroppedItem>();
        i.itemId = itemId;
        i.itemAmount = itemAmount;
        if(drillEffect != null) {
            Instantiate(drillEffect, transform.position + offset, Quaternion.identity);
        }
    }
}
