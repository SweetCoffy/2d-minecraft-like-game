using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class block : MonoBehaviour
{
    public float minimumMiningPower = 1;
    public float breakTime = 1f;
    public float breakProgress;
    public int dropItem;
    public int dropAmount = 1;
    Color originalColor;
    public int liquidLevel = 8;
    public bool fluid;
    public bool plant;
    public int maxGrow;
    public int grow;
    public float growTime;
    public bool breakOnGravityUpdate = false;
    public bool blockGravity;
    float timeToGrow;
    public string liquidName = "water";
    public LayerMask canCollideWith;
    public LayerMask cantCollideWith;
    public LayerMask waterMask;
    public LayerMask blocks;
    public float entityDamage;
    public float gravityUpdateRate = 10;
    public float liquidUpdateRate = 5;
    private Sprite[] cachedTextures;
    private SpriteRenderer s;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<SpriteRenderer>();
        originalColor = s.color;
        breakProgress = breakTime;
        timeToGrow = Time.time + growTime;
        if(blockGravity) {
            InvokeRepeating("Fall", 1 / gravityUpdateRate, 1 / gravityUpdateRate);
        }

        if (fluid) {
            cachedTextures = new Sprite[9];
            for (int i = 0; i < cachedTextures.Length; i++) {
                cachedTextures[i] = Resources.Load<Sprite>("Textures/" + liquidName + "-" + liquidLevel);
            }
            InvokeRepeating("LiquidUpdate", 1 / liquidUpdateRate, 1 / liquidUpdateRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if(plant) {

            if(grow < maxGrow) {
                if(Time.time >= timeToGrow)  {
                    Grow();
                }
            }
        
        }
        




    }
     void spawnItem(Item itemToSpawn) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position, Quaternion.identity);
            spawnedItem.GetComponent<droppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<droppedItem>().itemAmount = itemToSpawn.amount;
    }

    void OnMouseOver() {
        if(GetComponent<Crafter>() != null) {
            return;
        }
        
        ShowInfo();
    }

    void OnMouseExit() {
        if(GetComponent<Crafter>() != null) {
            return;
        }
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = "";
    }

    public void damage(float miningPower, float dmg) {
        if(miningPower >= minimumMiningPower) {
            breakProgress -= miningPower - minimumMiningPower + .5f + dmg;
            
            
        }

        if(breakProgress <= 0) {
            if(dropItem > -1 && dropAmount > 0) {
                spawnItem(new Item(dropItem, dropAmount));
                
            }
            Destroy(gameObject);
        }
    }

    void OnMouseDown() {
        GameObject.Find("Player").GetComponent<entity>().mineBlock(this);
    }

    public void Grow() {
       timeToGrow = Time.time + growTime;
        if(Physics2D.OverlapBox(transform.position + Vector3.up, transform.localScale * .9f, 0) != null) {
            return;
        }
        block grownBlock = Instantiate(gameObject, transform.position + Vector3.up, transform.rotation).GetComponent<block>();
        plant = false;
        grownBlock.grow++;
    }
    void Fall() {
        
        if(Physics2D.OverlapBox(transform.position - Vector3.up, transform.localScale * .9f, 0, canCollideWith) == null) {
            if (breakOnGravityUpdate) damage(999999999, 999999);
            transform.position -= Vector3.up;
        }
    }
    
    public void LiquidUpdate() {
        if (liquidLevel > 7) liquidLevel = 7;
        Collider2D left = Physics2D.OverlapBox(transform.position - transform.right, transform.localScale * .9f, 0, canCollideWith);
        Collider2D right = Physics2D.OverlapBox(transform.position + transform.right, transform.localScale * .9f, 0, canCollideWith);
        /*
            if (left != null) {
                block b = left.GetComponent<block>();
                if (b != null) {
                    if (liquidLevel < b.liquidLevel - 1) {
                        Destroy(gameObject);
                    }
                }
            }
            if (right != null) {
                block b = right.GetComponent<block>();
                if (b != null) {
                    if (liquidLevel < b.liquidLevel - 1) {
                        Destroy(gameObject);
                    }
                }
            }*/
        if(Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, canCollideWith) == null) {
            GameObject flowing = Instantiate(gameObject, transform.position - transform.up, transform.rotation);
            flowing.GetComponent<block>().liquidLevel = 8;
        }
        if(Physics2D.OverlapBox(transform.position, transform.localScale * .9f, 0, blocks) != null) {
            Destroy(gameObject);
        }
        
        if(liquidLevel > 0) {            
            if(right == null && Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, ~cantCollideWith) != null) {
                GameObject flowing = Instantiate(gameObject, transform.position + transform.right, transform.rotation);
                flowing.GetComponent<block>().liquidLevel = liquidLevel - 1;
            }
            if(left == null && Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, ~cantCollideWith) != null) {
                GameObject flowing = Instantiate(gameObject, transform.position - transform.right, transform.rotation);
                flowing.GetComponent<block>().liquidLevel = liquidLevel - 1;
            }
            


        }
        if(Physics2D.OverlapBox(transform.position + transform.up, transform.localScale * .9f, 0, waterMask) != null) {
            liquidLevel = 8;
        }
        if (Physics2D.OverlapBox(transform.position + transform.up, transform.localScale * .9f, 0, waterMask) == null) {
            liquidLevel = 7;
        }
        
        s.sprite = cachedTextures[liquidLevel];
    }
    
    public virtual void ShowInfo() {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = $"Block: \n{breakProgress} / {breakTime}";
    }

    void OnTriggerStay2D(Collider2D col) {
        
        if (entityDamage <= 0)
            return;
        
        entity e = col.GetComponent<entity>();

        
        
        if (e != null) {
            e.takeDamage(entityDamage * Time.deltaTime, false);
        }
    }
}


public class ItemSpawning{
    public static void Spawn(Item itemToSpawn, Vector3 position) {
            GameObject spawnedItem = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), position, Quaternion.Euler(0, 0, 0));
            spawnedItem.GetComponent<droppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<droppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode2D.Impulse);
    }
}

