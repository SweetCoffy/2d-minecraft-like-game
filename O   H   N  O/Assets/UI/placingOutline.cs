using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placingOutline : MonoBehaviour
{
    player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
