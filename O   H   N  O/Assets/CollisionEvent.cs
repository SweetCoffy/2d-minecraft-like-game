using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{

    public UnityEvent onTriggerEnter;

    void OnTriggerEnter2D(Collider2D col) {
        entity e = col.GetComponent<entity>();

        if (e) {
            if (onTriggerEnter != null) {
                onTriggerEnter.Invoke();
            }
        }
    }
    
}
