using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomExtractor : MonoBehaviour
{
    public float drillTime = 2;
    float drillProgress;
    public GameObject drillEffect;
    public int[] drops;
    public Vector3 itemOffset = Vector3.up;

    public int dropAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(drillProgress < drillTime) {
            drillProgress += Time.deltaTime;
        }
        if(drillProgress >= drillTime) {
            drillProgress = 0;
            ItemSpawning.Spawn(new Item(drops[(int)Mathf.Floor(Random.Range(0, drops.Length))], dropAmount), transform.position + itemOffset);
            if(drillEffect != null) {
                Instantiate(drillEffect, transform.position + itemOffset, Quaternion.identity);
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + itemOffset, Vector3.one);
    }
}
