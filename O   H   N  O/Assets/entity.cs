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
            takeDamage(dryDamageRate*Time.deltaTime);
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

    public void pickup(int itemId, int itemAmount) {
        if(storedItems.Count <= 0) {
            storedItems.Add(new Item(itemId, itemAmount));
            lastitemUpdate = storedItems.Count - 1;
            return;
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
            
        } else if (storedItems.Count+ 1 < itemCapacity - 1){
            storedItems.Add(new Item(itemId, itemAmount));
            lastitemUpdate = storedItems.Count - 1;
            
        }
    }


    public void startBlockPlace(GameObject blockToPlace, int x, int y) {
        Instantiate(blockToPlace, new Vector3(x, y, 0), transform.rotation);
    }

    public void pickup(int itemId, int itemAmount, List<float> properties) {
        
    if(storedItems.Count <= 0) {
            storedItems.Add(new Item(itemId, itemAmount, properties));
            Debug.Log("AAAA");
            lastitemUpdate = storedItems.Count - 1;
            return;
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
            
        } else if (storedItems.Count+ 1 < 16) {
            storedItems.Add(new Item(itemId, itemAmount, properties));
            lastitemUpdate = storedItems.Count - 1;
        }
    }
    
    
    
    
    public void jump(float force = 10) {
        if(!canJump) {
            force = 0;
        }
        rb.velocity += Vector2.up * force;

    }
    
    public void useItem(int item) {
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
    public void spawnItem(Item itemToSpawn, Vector3 offset) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position + offset, transform.rotation);
            spawnedItem.GetComponent<droppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<droppedItem>().itemAmount = itemToSpawn.amount;
            spawnedItem.GetComponent<droppedItem>().properties = itemToSpawn.properties;
    }

    public void dropItem(int index) {
        if(index > storedItems.Count - 1) {
            return;
        }

        spawnItem(storedItems[index], transform.localScale.normalized*1.5f);
        storedItems.RemoveAt(index);
        lastitemUpdate = index;

    }
    
    
    
    public void takeDamage(float amount = 0) {
        health -= amount;
        if(health <= 0) {
            kill();
        }
        
        }

    void kill() {
        Debug.Log("u ded lol");
    }

    public void setSelectedItem(int value = 0) {
        
        if(value < 0) {
            value = 16;
        } else if (value > 16) {
            value = 0;
        } 
        
        selectedItem = value;
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
        properties.Add(0);
        properties.Add(0);
    }

    public Item(int itemId, int itemAmount, List<float> itemProperties) {
        id = itemId;
        amount = itemAmount;
        properties = itemProperties;
    }
}
