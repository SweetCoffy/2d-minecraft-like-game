using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(entity))]
public class cursedAi : MonoBehaviour
{
    
    entity e;
    public Transform target;

    
    // Start is called before the first frame update
    void Start()
    {
        e = GetComponent<entity>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Mathf.Round(transform.position.x) < Mathf.Round(target.position.x)) {
            e.movementHorizontal(e.movementSpeed);
        } else {
            e.movementHorizontal(-e.movementSpeed);
        }

        if(target.position.y > transform.position.y) {
            e.jump();
        }
    
    
    }
}
