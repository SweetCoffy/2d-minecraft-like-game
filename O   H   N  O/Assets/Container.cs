﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(entity))]
public class Container : MonoBehaviour
{
    entity e;
    public LayerMask mask;
    public float dropRate = 5;
    bool canDrop = false;
    // Start is called before the first frame update
    void Start()
    {
        e = GetComponent<entity>();
        InvokeRepeating("Output", 0, 1/dropRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapBox((Vector2)(transform.position + transform.right), Vector2.one * 0.9f, 0, mask) != null) {
            itemduct duct = Physics2D.OverlapBox((Vector2)(transform.position + transform.right), Vector2.one * 0.9f, 0, mask).GetComponent<itemduct>();
            if(duct != null) {
                canDrop = true;
            } else {
                canDrop = false;
            }
        } else {
            canDrop = false;
        }
        
    }

    void Output() {
        if(canDrop) {
            e.dropItem(0, false, (Vector2)transform.right);
        }
    }
}