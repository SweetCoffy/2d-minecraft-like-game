using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class entity : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpForce = 10;
    public Vector3 placeBlockPosition;
    public float maxHealth = 10; public float maxThirst = 10;
    float health, thirst;
    public float itemCooldown = .5f;
    private float cooldown = 0;
    public float thirstDrainRate = .5f;
    public int itemCapacity = 17;
    public bool canRespawn = false;
    public Vector3 respawnPoint = Vector3.zero;
    public GameObject deathEffect;
    public GameObject damageEffect;
    public bool keepInventory = false;
    

    public int lastitemUpdate;

    public float dryDamageRate = .5f;
    public float getStat(string stat) {
        if(stat == "health") {
                return health;

        } else if (stat == "thirst" ) {
            return thirst;
        } else {
            return 0;
        }
    }
    public float getMaxStat(string stat) {
        if(stat == "health") {
            return maxHealth;
        } else if(stat == "thirst") {
            return maxThirst;
        } else {
            return 0;
        }
    }
    bool canJump;
    public List<Item> storedItems = new List<Item>();
    int selectedItem = 0;
    public int getSelectedItem() {
        return selectedItem;
    }
    public Rigidbody2D rb;
    public float getHealth(bool normalize = false) {
        if(normalize) {
            return health/maxHealth;
        } else {
            return health;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //storedItems.Add(new Item(0, 5));
        health = maxHealth;
        thirst = maxThirst;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(thirst <= 0) {
            takeDamage(dryDamageRate*Time.deltaTime, false);
        }
        if(thirst > 0) {
            thirst = Mathf.Clamp(thirst - (thirstDrainRate * Time.deltaTime), 0, maxThirst );
        }
        if(cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        canJump = rb.velocity.y <0.02 && rb.velocity.y > -0.02;
        rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity * Vector2.up, .2f);
    
    if(selectedItem > storedItems.Count-1) {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        return;
    }
    transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/item/item-" + storedItems[selectedItem].id );
    
    }
    
    public void movementHorizontal(float speed) {
        rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        if(speed > 0) {
            transform.localScale = new Vector3( Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if(speed < 0) {
            transform.localScale = new Vector3( -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public bool pickup(int itemId, int itemAmount) {
        if(storedItems.Count <= 0) {
            storedItems.Add(new Item(itemId, itemAmount));
            lastitemUpdate = storedItems.Count - 1;
            return true;
        }
        
        bool hasItem = false;
        int i = 0;
        foreach(Item currItem in storedItems) {
            i++;
            if(currItem.id == itemId) {
                hasItem = true;
                break;
            } 
        }
        if(hasItem) {
            storedItems[Mathf.Clamp(i-1, 0, storedItems.Count)].amount += itemAmount;
            lastitemUpdate = Mathf.Clamp(i-1, 0, storedItems.Count);
            return true;
            
        } else if (storedItems.Count+ 1 < itemCapacity - 1){
            storedItems.Add(new Item(itemId, itemAmount));
            lastitemUpdate = storedItems.Count - 1;
            return true;   
        } else {
            return false;
        }
       
    }


    public void startBlockPlace(GameObject blockToPlace, int x, int y) {
        Instantiate(blockToPlace, new Vector3(x, y, 0), transform.rotation);
    }

    public bool pickup(int itemId, int itemAmount, List<float> properties) {
        
    if(storedItems.Count <= 0) {
            storedItems.Add(new Item(itemId, itemAmount, properties));
            Debug.Log("AAAA");
            lastitemUpdate = storedItems.Count - 1;
            return true;
        }
        
        bool hasItem = false;
        int i = 0;
        foreach(Item currItem in storedItems) {
            i++;
            if(currItem.id == itemId) {
                hasItem = true;
                break;
            } 
        }
        if(hasItem) {
            storedItems[Mathf.Clamp(i-1, 0, storedItems.Count)].amount += itemAmount;
            lastitemUpdate = Mathf.Clamp(i-1, 0, storedItems.Count);
            return true;
            
        } else if (storedItems.Count+ 1 < itemCapacity - 1) {
            storedItems.Add(new Item(itemId, itemAmount, properties));
            lastitemUpdate = storedItems.Count - 1;
        } else {
            return false;
        }
        return false;
    }
    
    
    
    
    public void jump(float force = 10) {
        if(!canJump) {
            force = 0;
        }
        rb.velocity += Vector2.up * force;

    }
    
    public void useItem(int item, System.Nullable<Vector2> postion = null) {
        if(item > storedItems.Count - 1 || cooldown > 0) {
            return;
        }
        cooldown = itemCooldown;
        
        Item currItem = storedItems[item];
        if(currItem.id == 0) {
            health = Mathf.Clamp(health + Mathf.Round(Random.Range(0, 1)), 0, maxHealth);
            thirst += Random.Range(1, 5);
            takeDamage(Mathf.Round(Random.Range(1, 2)));
            spawnItem(new Item(2, 1));
            storedItems[item].amount -= 1;
            lastitemUpdate = item;

        }
        if(currItem.id == 1) {
            health = Mathf.Clamp(health + Mathf.Round(Random.Range(1, 3)), 0, maxHealth);
            thirst += Random.Range(5, 10);
            spawnItem(new Item(2, 1));
            storedItems[item].amount -= 1;
            lastitemUpdate = item;
        }
        if(currItem.id == 28) {
            health = Mathf.Clamp(health + Mathf.Round(Random.Range(2, 10)), 0, maxHealth);
            storedItems[item].amount -= 1;
            lastitemUpdate = item;
        }

        if (currItem.id == 40) {
            
            
            Vector2 pos = (Vector2)postion;

            if (pos != null) {
                if (pos.x > transform.position.x) {
                    transform.localScale = new Vector3( Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                } else if (pos.x < transform.position.x) {
                    transform.localScale = new Vector3( -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                Transform heldItem = transform.GetChild(0);
                
                Vector2 dir = (Vector2)transform.position - pos;
                
                float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                
                if (transform.localScale.x > 0) {
                    heldItem.rotation = Quaternion.Euler(0, 0, rot - 180);
                } else {
                    heldItem.rotation = Quaternion.Euler(0, 0, rot);
                }

                if (ConsumeItemType(ItemID.Rock, 0.1f)) {
                    GameObject b = Resources.Load<GameObject>("Prefabs/Bullet");

                    GameObject bulletObject = Instantiate(b, heldItem.position, Quaternion.Euler(0, 0, rot - 180));

                    Collider2D col = bulletObject.GetComponent<Collider2D>();

                    // Bullet bullet = bulletObject.GetComponent<Bullet>();

                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);

                    cooldown = itemCooldown * 0.1f;
                }
                

                
                
            }
        }

        if (currItem.id == 42) {
            maxHealth += 20;
            health += 20;
            lastitemUpdate = item;
            consumeItem(item);
        }

        if (currItem.id == 43) {
            maxThirst += 20;
            thirst += 20;
            lastitemUpdate = item;
            consumeItem(item);
        }
             
        if(storedItems[item].amount <= 0 ) {
            storedItems.RemoveAt(item);
        }
    }

    public void mineBlock(block blockToMine) {
        if(selectedItem > storedItems.Count -1) {
            return;
        }
        blockToMine.damage(storedItems[selectedItem].properties[0], storedItems[selectedItem].properties[1] );
    }
    
    public void consumeItem(int item) {
        if(item > storedItems.Count - 1 || storedItems[item].amount <= 0) {
            return;
        }
        lastitemUpdate = item;
        storedItems[item].amount -= 1;
        Item currItem = storedItems[item];   
        if(storedItems[item].amount <= 0 ) {
            storedItems.RemoveAt(item);
        }
    }
    
    public void spawnItem(Item itemToSpawn) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position, transform.rotation);
            spawnedItem.GetComponent<droppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<droppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<droppedItem>().properties = itemToSpawn.properties;
    }

    public void spawnItem(Item itemToSpawn, float randomness) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position, transform.rotation);
            spawnedItem.GetComponent<droppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<droppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<droppedItem>().properties = itemToSpawn.properties;
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness)), ForceMode2D.Impulse);
    }

    public void spawnItem(Item itemToSpawn, Vector3 offset) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position + offset, transform.rotation);
            spawnedItem.GetComponent<droppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<droppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<droppedItem>().properties = itemToSpawn.properties;
    }

    public void dropItem(int index, bool droppAll) {
        if(index > storedItems.Count - 1) {
            return;
        }

        if(droppAll) {
            spawnItem(storedItems[index], transform.localScale.normalized*1.5f);
            storedItems.RemoveAt(index);
            lastitemUpdate = index;
        } else {
            spawnItem(new Item(storedItems[index].id, 1), transform.localScale.normalized*1.5f);
            consumeItem(index);
            lastitemUpdate = index;
        }
    }
    public void dropItem(int index, bool droppAll, Vector2 direction) {
        if(index > storedItems.Count - 1) {
            return;
        }

        
        
        if(droppAll) {
            spawnItem(storedItems[index], direction);
            storedItems.RemoveAt(index);
            lastitemUpdate = index;
        } else {
            spawnItem(new Item(storedItems[index].id, 1), direction);
            consumeItem(index);
            lastitemUpdate = index;
        }
    }

    public void dropItemAll(int index) {
        dropItem(index, true);
    }
    
    
    
    public void takeDamage(float amount = 0, bool spawnBlood = true) {
        health -= amount;
        if (damageEffect && spawnBlood) {
            Instantiate(damageEffect, transform.position, transform.rotation);
        }
        
        if(health <= 0) {
            kill();
        }
        
        }

    void kill() {
        
        
        if (deathEffect) {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        
        if (!keepInventory) {
            foreach(Item it in storedItems) {
                spawnItem(it, 10f);
            }

            storedItems = new List<Item>();
        }
        

        
        if (!canRespawn) {
            Destroy(gameObject);
        } else {

            transform.position = (Vector3)respawnPoint;
            health = maxHealth;
            thirst = maxThirst;
        }
        
    }



    public bool ConsumeItemType(int id, float chance = 1) {
        for (int i = 0; i < storedItems.Count; i++) {
            if (storedItems[i].id == id) {
                
                if (Random.Range(0, 1) < chance) {
                    consumeItem(i);
                    lastitemUpdate = i;
                }
                
                
                return true;
            }
        }
        return false;
    }
    
    public void setSelectedItem(int value = 0) {
        
        if(value < 0) {
            value = itemCapacity - 1;
        } else if (value > itemCapacity - 1) {
            value = 0;
        } 
        
        selectedItem = value;
        transform.GetChild(0).rotation = Quaternion.identity;
    }

}

[System.Serializable]
public class Item {

    public int id;
    public int amount;
    public List<float> properties = new List<float>();
    
    
    
    public Item(int itemId, int itemAmount) {
        id = itemId;
        amount = itemAmount;
        properties = new List<float>(2);
    }

    public Item(int itemId, int itemAmount, List<float> itemProperties) {
        id = itemId;
        amount = itemAmount;
        properties = itemProperties;
    }
}

public static class ItemID {
    public static int Rock = 4;
    public static int BasicPick = 3;
    public static int Bottle = 2;
    public static int PurifiedWater = 1;
    public static int Water = 0;
}
