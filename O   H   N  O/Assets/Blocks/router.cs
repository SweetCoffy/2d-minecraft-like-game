using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col) {
        droppedItem di = col.GetComponent<droppedItem>();
        Entity e = col.GetComponent<Entity>();

        
        if(di != null) {
            di.GetComponent<Rigidbody2D>().position = (Vector2)transform.position;
            di.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 a = Vector2.zero;
            Collider2D b = Physics2D.OverlapBox((Vector2)transform.position + (Vector2)transform.up, Vector2.one * .9f, 0, 1021);
            Collider2D c = Physics2D.OverlapBox((Vector2)transform.position + (Vector2)transform.right, Vector2.one * .9f, 0, 1021);
            Collider2D d = Physics2D.OverlapBox((Vector2)transform.position - (Vector2)transform.up, Vector2.one * .9f, 0, 1021);

            Collider2D[] sides = new Collider2D[]{b, c, d};
            List<Itemduct> possibleOnes = new List<Itemduct>();
            
            int i = 0;
            foreach(Collider2D side in sides) {
                
                if(side != null) {
                    if(side.GetComponent<Itemduct>() != null) {
                        possibleOnes.Add(side.GetComponent<Itemduct>());
                    }
                }

                i++;
            }
            
           if(possibleOnes.Count < 1) {
               return;
           }
            a = possibleOnes[(int)Mathf.Floor(Random.Range(0, possibleOnes.Count))].transform.position;
            di.GetComponent<Rigidbody2D>().position = a;
            
        }

        
    }
}
