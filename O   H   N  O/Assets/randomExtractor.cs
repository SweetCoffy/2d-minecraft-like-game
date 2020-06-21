using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomExtractor : MonoBehaviour
{
    public float drillTime = 2;
    float drillProgress;
    public int[] drops;

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
            item.spawn(new Item(drops[(int)Mathf.Floor(Random.Range(0, drops.Length))], dropAmount), transform.position + transform.up);
        }
    }
}
