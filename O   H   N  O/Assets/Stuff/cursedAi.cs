using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class CursedAI : MonoBehaviour
{
    
    Entity e;
    public Transform target;
    public bool ignoreCollisions = true;
    public ContactFilter2D filter;
    public bool mining = false;
    public bool follow = true;

    
    // Start is called before the first frame update
    void Start()
    {
        e = GetComponent<Entity>();
        if(!ignoreCollisions) {
            return;
        }
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(follow) {
            if(transform.position.x < target.position.x - (target.localScale.x * 1.5)) {
                e.Move(e.movementSpeed);
            } 
            if(transform.position.x > target.position.x + (target.localScale.x * 1.5)) {
                e.Move(-e.movementSpeed);
            }
            if(target.position.y - (target.localScale.y * 1.5) > transform.position.y) {
                e.jump(e.jumpForce);
            }
        }
        int i3 = 0;
        if(e.GetStat("thirst") / e.GetMaxStat("thirst") > .25f) {
            return;
        }
        int i = 0;
        foreach(Item item in e.storedItems) {
            if(item.id == 1) {
                break;
            }
            i++;
        }
        e.setSelectedItem(i);
        e.useItem(e.GetSelectedItem());
    }

    public void SetAutoMining(bool value) {
        mining = value;
    }
    public void SetFollow(bool value) {
        follow = value;
    }
}
