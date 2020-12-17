using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour
{
    public float movementSpeed = 10;
    public float jumpForce = 10;
    public Vector3 placeBlockPosition;
    public float drag = 5;
    public float maxHealth = 10; 
    public float maxThirst = 10;
    float health, thirst;
    public float maxFallDmg = 100;
    public float itemCooldown = .5f;
    public Block selectedBlock;
    private float cooldown = 0;
    public float thirstDrainRate = .5f;
    public float minFallDist = 50;
    public float dmgPerFallDistUnit = 0.4f;
    public int itemCapacity = 17;
    public float maxAir = 30;
    public bool canBreathe = true;
    [HideInInspector] public float air;
    public bool canRespawn = false;
    public Vector3 respawnPoint = Vector3.zero;
    public GameObject deathEffect;
    private float fallDist = 0;
    public GameObject teleportParticles;
    public GameObject damageEffect;
    float manaRecoverTimer = 0;
    public float maxMana = 20;
    public float airRecoverRate = 10;
    public float manaRecoverRate = 20f;
    float mana;
    public bool moveAirBar = true;
    public CanvasGroup airThing;
    public Image airBar;
    public bool keepInventory = false;
    public List<Item> startingItems = new List<Item>();
    public bool respawnWithPickaxe = false;
    Color defaultAirBarColor;
    public int lastitemUpdate;
    public Text countdown;
    public float thihstrstDamageRate = .5f;
    bool canJump;
    public List<Item> storedItems = new List<Item>();
    int selectedItem = 0;
    public int GetSelectedItem() {
        return selectedItem;
    }
    public Rigidbody2D rb;
    public void ConsumeMana(float amount) {
        manaRecoverTimer = 0.5f;
        mana -= amount;
    }
    public float GetMaxStat(string stat) {
        if (stat == "health") return maxHealth;
        else if (stat == "thirst") return maxThirst;
        else return maxMana;
    }
    public float GetStat(string stat) {
        if (stat == "health") return health;
        else if (stat == "thirst") return thirst;
        else return mana;
    }
    void Start()
    {
        if (airBar) defaultAirBarColor = airBar.color;
        air = maxAir;
        health = maxHealth;
        thirst = maxThirst;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (canBreathe && air < maxAir) air += airRecoverRate * Time.deltaTime;
        else if (!canBreathe && air > 0) air -= Time.deltaTime;
        if (air <= 0) Kill();
        if (airBar && airThing) {
            if (moveAirBar) airThing.transform.position = transform.position + (Vector3.one * 1.5f);
            if (air < maxAir) airThing.alpha = 1;
            else airThing.alpha *= 0.8f;
            airBar.fillAmount = air / maxAir;
            if (countdown && air < 5) {
                countdown.enabled = true;
                countdown.text = Mathf.Clamp(Mathf.Ceil(air), 0, 5) + "";
            } else countdown.enabled = false;
        }
        if (manaRecoverTimer > 0) {
            manaRecoverTimer -= Time.deltaTime;
        }
        if (manaRecoverTimer <= 0) {
            mana += manaRecoverRate * Time.deltaTime;
            if (mana > maxMana) {
                mana = maxMana;
            }
        }
        if(thirst <= 0) {
            TakeDamage(thihstrstDamageRate*Time.deltaTime, false);
        }
        if(thirst > 0) {
            thirst = Mathf.Clamp(thirst - (thirstDrainRate * Time.deltaTime), 0, maxThirst );
        }
        if(cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        canJump = rb.velocity.y <0.02 && rb.velocity.y > -0.02;
        rb.velocity = Vector2.Lerp(rb.velocity, rb.velocity * Vector2.up, drag * Time.deltaTime);
    
        if(selectedItem > storedItems.Count-1) {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            return;
        }
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/item/item-" + storedItems[selectedItem].id );
    }
    
    public void Move(float speed) {
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
            if (storedItems[Mathf.Clamp(i-1, 0, storedItems.Count)].properties.Count < 2) {
                storedItems[Mathf.Clamp(i-1, 0, storedItems.Count)].properties.Add(0);
                storedItems[Mathf.Clamp(i-1, 0, storedItems.Count)].properties.Add(0);
            }
            return true;
            
        } else if (storedItems.Count+ 1 < itemCapacity - 1){
            storedItems.Add(new Item(itemId, itemAmount));
            lastitemUpdate = storedItems.Count - 1;
            return true;   
        } else {
            return false;
        }
       
    }
    public bool canPlace = true;
    public bool startBlockPlace(GameObject blockToPlace, int x, int y) {
        if (!blockToPlace) return false;
        if (!canPlace) return false;
        Instantiate(blockToPlace, new Vector3(x, y, 0), transform.rotation);
        canPlace = false;
        return true;
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
    void FixedUpdate() {
        if (rb.velocity.y < -20) {
            rb.velocity = Vector2.up * -20;
        }
        fallDist -= rb.velocity.y * Time.deltaTime;
        if (canJump) {
            var dmg = Mathf.Clamp((fallDist - minFallDist) * dmgPerFallDistUnit, 0, maxFallDmg);
            if (dmg > 1) TakeDamage(dmg);
            fallDist = 0;
        }
    }
    public void Jump(float force = 10) {
        if(!canJump) {
            force = 0;
        }
        rb.velocity += Vector2.up * force;

    }
    public void UseItem(int item, System.Nullable<Vector2> position = null) {
        if(item > storedItems.Count - 1 || cooldown > 0) {
            return;
        }
        cooldown = itemCooldown;
        
        Item currItem = storedItems[item];
        if(currItem.id == 0) {
            health = Mathf.Clamp(health + Mathf.Round(Random.Range(0, 1)), 0, maxHealth);
            thirst += Random.Range(1, 5);
            TakeDamage(Mathf.Round(Random.Range(1, 2)));
            SpawnItem(new Item(2, 1));
            storedItems[item].amount -= 1;
            lastitemUpdate = item;

        }
        if(currItem.id == 1) {
            health = Mathf.Clamp(health + Mathf.Round(Random.Range(1, 3)), 0, maxHealth);
            thirst += Random.Range(5, 10);
            SpawnItem(new Item(2, 1));
            storedItems[item].amount -= 1;
            lastitemUpdate = item;
        }
        if(currItem.id == 28) {
            health = Mathf.Clamp(health + Mathf.Round(Random.Range(2, 10)), 0, maxHealth);
            storedItems[item].amount -= 1;
            lastitemUpdate = item;
        }

        if (currItem.id == 40) {
            
            
            Vector2 pos = (Vector2)position;

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
                    for (int i = 0; i < 6; i++) {
                        GameObject b = Resources.Load<GameObject>("Prefabs/Bullet");

                        GameObject bulletObject = Instantiate(b, heldItem.position, Quaternion.Euler(0, 0, rot - 180 + Random.Range(-12, 12)));

                        Collider2D col = bulletObject.GetComponent<Collider2D>();

                        Bullet bullet = bulletObject.GetComponent<Bullet>();
                        bullet.shooter = gameObject;
                        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);

                        cooldown = itemCooldown * 1.2f;
                    }
                }
                

                
                
            }
        }

        if (currItem.id == 42) {
            maxHealth += 20;
            health += 20;
            lastitemUpdate = item;
            ConsumeItem(item);
        }

        if (currItem.id == 43) {
            maxThirst += 20;
            thirst += 20;
            lastitemUpdate = item;
            ConsumeItem(item);
        }

        if (currItem.id == 44) {
            Debug.Log(this);
            Debug.Log(position);
            if (position == null) 
                return;
            
            Instantiate(teleportParticles, transform.position, Quaternion.identity);
            transform.position = (Vector3)((Vector2)position + (Vector2.up * 0.5f));
            if (mana < 20) {
                TakeDamage(10);
                ConsumeMana(0);
            } else {
                ConsumeMana(20);
            }
            Instantiate(teleportParticles, transform.position, Quaternion.identity);
            
        }
             
        if(storedItems[item].amount <= 0 ) {
            storedItems.RemoveAt(item);
        }
    }

    public void MineBlock(Block blockToMine) {
        if(selectedItem > storedItems.Count -1) {
            return;
        }
        if (!blockToMine) return;
        blockToMine.Damage(storedItems[selectedItem].properties[0], storedItems[selectedItem].properties[1], Time.deltaTime);
    }
    
    public void ConsumeItem(int item) {
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
    
    public void SpawnItem(Item itemToSpawn) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position, transform.rotation);
            spawnedItem.GetComponent<DroppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<DroppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<DroppedItem>().properties = itemToSpawn.properties;
    }

    public void SpawnItem(Item itemToSpawn, float randomness) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position, transform.rotation);
            spawnedItem.GetComponent<DroppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<DroppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<DroppedItem>().properties = itemToSpawn.properties;
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness)), ForceMode2D.Impulse);
    }

    public void SpawnItem(Item itemToSpawn, Vector3 offset) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position + offset, transform.rotation);
            spawnedItem.GetComponent<DroppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<DroppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<DroppedItem>().properties = itemToSpawn.properties;
    }
    public void SpawnItem(Item itemToSpawn, Vector3 offset, Vector2 force) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position + offset, transform.rotation);
            spawnedItem.GetComponent<DroppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<DroppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<DroppedItem>().properties = itemToSpawn.properties;
            spawnedItem.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    public void DropItem(int index, bool droppAll) {
        if(index > storedItems.Count - 1) {
            return;
        }

        if(droppAll) {
            SpawnItem(storedItems[index], transform.localScale.normalized*1.5f);
            storedItems.RemoveAt(index);
            lastitemUpdate = index;
        } else {
            SpawnItem(new Item(storedItems[index].id, 1), transform.localScale.normalized.x * Vector3.right * 1.2f, (Vector2)transform.localScale.normalized * Vector2.right * 7);
            ConsumeItem(index);
            
            lastitemUpdate = index;
        }
    }
    public void DropItem(int index, bool droppAll, Vector2 direction) {
        if(index > storedItems.Count - 1) {
            return;
        }

        
        
        if(droppAll) {
            SpawnItem(storedItems[index], direction);
            storedItems.RemoveAt(index);
            lastitemUpdate = index;
        } else {
            SpawnItem(new Item(storedItems[index].id, 1), direction);
            ConsumeItem(index);
            lastitemUpdate = index;
        }
    }

    public void DropItemAll(int index) {
        DropItem(index, true);
    }
    
    
    
    public void TakeDamage(float amount = 0, bool spawnBlood = true) {
        health -= amount;
        if (damageEffect && spawnBlood) {
            Instantiate(damageEffect, transform.position, transform.rotation);
        }
        
        if(health <= 0) {
            Kill();
        }
        
        }

    void Kill() {
        
        
        if (deathEffect) {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
        
        int i = 0;
        
        List<Item> oldItems = storedItems;

        foreach(Item it in oldItems) {
            SpawnItem(it, 5f);
            
            
            i++;
        }

        storedItems.RemoveRange(0, storedItems.Count);
        
        if (canRespawn) {
            transform.position = (Vector3)respawnPoint;
            health = maxHealth;
            thirst = maxThirst;
            
            if (respawnWithPickaxe) {
                storedItems.Add(new Item(ItemID.BasicPick, 1));
                storedItems[0].properties.Add(1);
                storedItems[0].properties.Add(2);
            }

        } else {
            Destroy(gameObject);
        }
            
        
        


       


  
        
        
        

        

        
    }



    public bool ConsumeItemType(int id, float chance = 1) {
        for (int i = 0; i < storedItems.Count; i++) {
            if (storedItems[i].id == id) {
                
                if (Random.Range(0, 1) < chance) {
                    ConsumeItem(i);
                    lastitemUpdate = i;
                }
                
                
                return true;
            }
        }
        return false;
    }
    
    public void SetSelectedItem(int value = 0) {
        
        if(value < 0) {
            value = itemCapacity - 1;
        } else if (value > itemCapacity - 1) {
            value = 0;
        } 
        
        selectedItem = value;
        
        if (value != 40)
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
    public static int ClinWater = 1;
    public static int Water = 0;
}
