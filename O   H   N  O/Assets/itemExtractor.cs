using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemExtractor : MonoBehaviour
{
    public Vector3 offset;
    public int itemId;
    public int itemAmount;
    public float waitTime = 1;
    float progress;
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
        droppedItem i = spawned.GetComponent<droppedItem>();
        i.itemId = itemId;
        i.itemAmount = itemAmount;
    }
}
