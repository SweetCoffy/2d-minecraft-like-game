using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour
{
    public CraftingRecipe recipe;
    List<Item> storedItems = new List<Item>(); 
    public float smelterTier = 1; //A.K.A. Progress multiplier
    float smeltProgress;
    public Vector3 spawnedItemOffset;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Item item in recipe.input) {
            storedItems.Add(new Item(item.id, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        bool b = false;
        foreach(Item item in storedItems) {
            if(item.amount >= recipe.input[i].amount ) {
                b = true;
            } else {
                b = false;
            }
            i++;
        }
        if(b) {
            if(smeltProgress < recipe.craftDuration) {
                smeltProgress += Time.deltaTime * smelterTier;
            }
            if(smeltProgress >= recipe.craftDuration) {
                ItemSpawning.Spawn(recipe.output, transform.position + spawnedItemOffset);
                smeltProgress = 0;
                int i2 = 0;
                foreach(Item item in storedItems) {
                    item.amount -= recipe.input[i2].amount;
                    i2++;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D col) {
        GameObject other = col.gameObject;
        int ii = 0;
        if(other.GetComponent<DroppedItem>() != null) {
            DroppedItem i = other.GetComponent<DroppedItem>();
            foreach(Item item in storedItems) {
            if(i.itemId == item.id) {
                storedItems[ii].amount += i.itemAmount;
                Destroy(other);
            }
            ii++;
            }
            

        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + spawnedItemOffset, Vector3.one);
    }
}
