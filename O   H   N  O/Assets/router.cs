using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class router : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col) {
        droppedItem di = col.GetComponent<droppedItem>();
        entity e = col.GetComponent<entity>();

        
        if(di != null) {
            di.GetComponent<Rigidbody2D>().position = (Vector2)transform.position;
            di.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 a = Vector2.zero;
            Collider2D b = Physics2D.OverlapBox((Vector2)transform.position + (Vector2)transform.up, Vector2.one * .9f, 0, 1021);
            Collider2D c = Physics2D.OverlapBox((Vector2)transform.position + (Vector2)transform.right, Vector2.one * .9f, 0, 1021);
            Collider2D d = Physics2D.OverlapBox((Vector2)transform.position - (Vector2)transform.up, Vector2.one * .9f, 0, 1021);

            Collider2D[] sides = new Collider2D[]{b, c, d};
            List<itemduct> possibleOnes = new List<itemduct>();
            
            int i = 0;
            foreach(Collider2D side in sides) {
                
                if(side != null) {
                    if(side.GetComponent<itemduct>() != null) {
                        possibleOnes.Add(side.GetComponent<itemduct>());
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
