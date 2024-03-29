﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DroppedItem : MonoBehaviour
{
    public int itemId;
    public int itemAmount;
    int oldItem;
    public List<float> properties = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        properties.Add(0);
        properties.Add(0);
        if (itemId > GameObject.Find("ItemManager").GetComponent<ItemManager>().itemTextures.Length - 1)
        {
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("ItemManager").GetComponent<ItemManager>().unknownTexture;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("ItemManager").GetComponent<ItemManager>().itemTextures[itemId];
        }
        if (itemId == 3)
        {
            properties[0] = 1;
            properties[1] = 2;
        }
        else if (itemId == 10)
        {
            properties[0] = 2;
            properties[1] = 2.75f;
        }
        else if (itemId == 21)
        {
            properties[0] = 3;
            properties[1] = 5;
        }
        else if (itemId == 34)
        {
            properties[0] = 5;
            properties[1] = 10;
        }
        else
        {
            properties[0] = 0;
            properties[1] = .1f;
        }




    }

    // Update is called once per frame

    void LateUpdate()
    {
        oldItem = itemId;
    }
    void Update()
    {
        if (oldItem != itemId)
        {
            if (itemId > GameObject.Find("ItemManager").GetComponent<ItemManager>().itemTextures.Length - 1)
            {
                GetComponent<SpriteRenderer>().sprite = GameObject.Find("ItemManager").GetComponent<ItemManager>().unknownTexture;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = GameObject.Find("ItemManager").GetComponent<ItemManager>().itemTextures[itemId];
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        GameObject other = col.gameObject;
        if (other.GetComponent<Entity>() != null)
        {
            Entity otherEntity = other.GetComponent<Entity>();
            if (properties.Count > 0)
            {
                if (otherEntity.pickup(itemId, itemAmount, properties))
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (otherEntity.pickup(itemId, itemAmount))
                {
                    Destroy(gameObject);
                }
            }


            Destroy(gameObject);
        }
    }





}
