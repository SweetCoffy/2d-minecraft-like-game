using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!cam)
            cam = Camera.main.gameObject;
        
        transform.position = cam.transform.position*parallaxEffect;
    }
}
