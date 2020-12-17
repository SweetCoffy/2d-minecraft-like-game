using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public float minimumMiningPower = 1;
    public float breakTime = 1f;
    public float breakProgress;
    public int DropItem;
    public int id = 0;
    public int dropAmount = 1;
    Color originalColor;
    public int liquidLevel = 8;
    public Color liquidColor;
    public float fallDistance = 1;
    public bool fluid;
    public bool flowing = true;
    public bool plant;
    public int maxGrow;
    public int grow;
    public Vector3 blockDetectionOffset = Vector3.zero;
    public Vector3 blockDetectionSize = Vector3.one * 0.9f;
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
    public bool waterlogable;
    public bool falling = true;
    
    
    
    // Start is called before the first frame update
    protected virtual void Start()
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
    protected virtual void Update()
    {
        if(plant) {

            if(grow < maxGrow) {
                if(Time.time >= timeToGrow)  {
                    Grow();
                }
            }
        
        }
    }
    void SpawnItem(Item itemToSpawn) {
        GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position, Quaternion.identity);
        spawnedItem.GetComponent<DroppedItem>().itemId = itemToSpawn.id;
        spawnedItem.GetComponent<DroppedItem>().itemAmount = itemToSpawn.amount;
    }

    protected virtual void OnMouseOver() {
        if(GetComponent<Crafter>() != null) {
            return;
        }
        
        ShowInfo();
    }

    protected virtual void OnMouseExit() {
        if(GetComponent<Crafter>() != null) {
            return;
        }
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = "";
    }

    public void Damage(float miningPower, float dmg, float multiplier = 1) {
        if(miningPower >= minimumMiningPower) {
            breakProgress -= (miningPower - minimumMiningPower + .5f + dmg) * multiplier;
            
            
        }

        if(breakProgress <= 0) {
            if(DropItem > -1 && dropAmount > 0) {
                SpawnItem(new Item(DropItem, dropAmount));
                
            }
            Destroy(gameObject);
        }
    }

    protected virtual void OnMouseDown() {
        GameObject.Find("Player").GetComponent<Entity>().MineBlock(this);
    }

    public virtual void Grow() {
       timeToGrow = Time.time + growTime;
        if(Physics2D.OverlapBox(transform.position + Vector3.up, transform.localScale * .9f, 0) != null) {
            return;
        }
        Block grownBlock = Instantiate(gameObject, transform.position + Vector3.up, transform.rotation).GetComponent<Block>();
        plant = false;
        grownBlock.grow++;
    }
    protected virtual void Fall() {
        if (!falling) return;
        Collider2D h = Physics2D.OverlapBox(transform.position - (Vector3.up * fallDistance) + blockDetectionOffset, blockDetectionSize, 0, canCollideWith, -90, 90);
        if(h == null || h.gameObject == gameObject) {
            if (breakOnGravityUpdate) Damage(999999999, 999999);
            transform.position -= Vector3.up * fallDistance;
        }
    }
    
    public void LiquidUpdate() {
        if (liquidLevel > 7) liquidLevel = 7;
        if (flowing) {
            SendMessage("OnLiquidUpdate");
            Collider2D left = Physics2D.OverlapBox(transform.position - transform.right, transform.localScale * .9f, 0, canCollideWith, -90, 90);
            Collider2D right = Physics2D.OverlapBox(transform.position + transform.right, transform.localScale * .9f, 0, canCollideWith, -90, 90);
            /*
                if (left != null) {
                    Block b = left.GetComponent<Block>();
                    if (b != null) {
                        if (liquidLevel < b.liquidLevel - 1) {
                            Destroy(gameObject);
                        }
                    }
                }
                if (right != null) {
                    Block b = right.GetComponent<Block>();
                    if (b != null) {
                        if (liquidLevel < b.liquidLevel - 1) {
                            Destroy(gameObject);
                        }
                    }
                }*/
            Collider2D _down = Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, canCollideWith, -90, 90);
            if(_down == null) {
                GameObject flowing = Instantiate(gameObject, transform.position - transform.up, transform.rotation);
                flowing.GetComponent<Block>().liquidLevel = 8;
            } else {
                Block b = _down.GetComponent<Block>();
                if (b) {
                    if (b.waterlogable) {
                        GameObject flowing = Instantiate(gameObject, transform.position - transform.up, transform.rotation);
                        flowing.GetComponent<Block>().liquidLevel = 8;
                    }
                }
            }
            Collider2D h = Physics2D.OverlapBox(transform.position, transform.localScale * .9f, 0, blocks, -90, 90);
            if(h != null) {
                Block b = h.GetComponent<Block>();
                if (b) {
                    if (!b.waterlogable) {
                        Destroy(gameObject);
                    }
                }
            }
            
            if(liquidLevel > 0) {            
                if(right == null && Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, ~cantCollideWith, -90, 90) != null) {
                    GameObject flowing = Instantiate(gameObject, transform.position + transform.right, transform.rotation);
                    flowing.GetComponent<Block>().liquidLevel = liquidLevel - 1;
                } else if (right != null){
                    Block b = right.GetComponent<Block>();
                    if (b) {
                        if (b.waterlogable) {
                            GameObject flowing = Instantiate(gameObject, transform.position + transform.right, transform.rotation);
                            flowing.GetComponent<Block>().liquidLevel = liquidLevel - 1;
                        }
                    }
                }
                if(left == null && Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, ~cantCollideWith, -90, 90) != null) {
                    GameObject flowing = Instantiate(gameObject, transform.position - transform.right, transform.rotation);
                    flowing.GetComponent<Block>().liquidLevel = liquidLevel - 1;
                } else if (left != null){
                    Block b = left.GetComponent<Block>();
                    if (b) {
                        if (b.waterlogable) {
                            GameObject flowing = Instantiate(gameObject, transform.position - transform.right, transform.rotation);
                            flowing.GetComponent<Block>().liquidLevel = liquidLevel - 1;
                        }
                    }
                }
                


            }
            if(Physics2D.OverlapBox(transform.position + transform.up, transform.localScale * .9f, 0, waterMask, -90, 90) != null) {
                liquidLevel = 8;
            }
            if (Physics2D.OverlapBox(transform.position + transform.up, transform.localScale * .9f, 0, waterMask, -90, 90) == null) {
                liquidLevel = 7;
            }
        }
        
        s.sprite = cachedTextures[liquidLevel];
    }
    
    public virtual void ShowInfo() {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = $"Block: \n{breakProgress} / {breakTime}";
    }
    void OnTriggerExit2D(Collider2D col) {
        if (!fluid)
            return;
        Entity e = col.GetComponent<Entity>();        
        if (e != null) {
            e.canBreathe = true;
        }
    }
    void OnTriggerStay2D(Collider2D col) {
        Entity e = col.GetComponent<Entity>();
        if (e && fluid) {e.canBreathe = false; if (e.airBar) e.airBar.color = liquidColor;}
        if (entityDamage <= 0)
            return;
        

        
        
        if (e != null) {
            e.TakeDamage(entityDamage * Time.deltaTime, false);
        }
    }
    void OnDrawGizmos() {
        if (blockGravity) Gizmos.DrawWireCube(transform.position - (Vector3.up * fallDistance) + blockDetectionOffset, blockDetectionSize);
    }
}


public class ItemSpawning{
    public static void Spawn(Item itemToSpawn, Vector3 position) {
            GameObject spawnedItem = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), position, Quaternion.Euler(0, 0, 0));
            spawnedItem.GetComponent<DroppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<DroppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode2D.Impulse);
    }
}

