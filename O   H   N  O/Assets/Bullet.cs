using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public float lifetime = 10;
    public float damage = 5;
    Rigidbody2D rb;
    public GameObject bloodEffect;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col) {
        entity e = col.gameObject.GetComponent<entity>();

        if (e != null) {
            e.takeDamage(damage);
            if (bloodEffect) {
                Instantiate(bloodEffect, transform.position, transform.rotation);
            }
        }
        
        
        Destroy(gameObject);
    }
}
