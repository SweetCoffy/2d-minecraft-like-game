using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public List<GameObject> affectedObjects;

    public bool useAfectedObjects = true;
    public UnityEvent onTriggerEnter;

    void OnTriggerEnter2D(Collider2D col) {
        if (!affectedObjects.Contains(col.gameObject) && useAfectedObjects) return;
        entity e = col.GetComponent<entity>();

        if (e) {
            if (onTriggerEnter != null) {
                onTriggerEnter.Invoke();
            }
        }
    }
    
}
