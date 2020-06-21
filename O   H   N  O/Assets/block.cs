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
    float timeToGrow;
    public string liquidName = "water";
    public LayerMask canCollideWith;
    public LayerMask cantCollideWith;
    public LayerMask waterMask;
    public LayerMask blocks;
    
    
    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        breakProgress = breakTime;
        timeToGrow = Time.time + growTime;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, originalColor, .35f);
        if(breakProgress <= 0) {
            if(dropItem > -1 && dropAmount > 0) {
                spawnItem(new Item(dropItem, dropAmount));
                
            }
            Destroy(gameObject);
        }
        if(plant) {

        if(grow < maxGrow) {
            if(Time.time >= timeToGrow)  {
                Grow();
            }
        }
            
        
        }
        
        
        if(!fluid) {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/" + liquidName + "-" + liquidLevel);

        if(Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, canCollideWith) == null) {
            GameObject flowing = Instantiate(gameObject, transform.position - transform.up, transform.rotation);
            flowing.GetComponent<block>().liquidLevel = 7;
        }
        if(Physics2D.OverlapBox(transform.position, transform.localScale * .9f, 0, blocks) != null) {
            Destroy(gameObject);
        }
        
        if(liquidLevel <= 0) {
            return;
        }
        if(Physics2D.OverlapBox(transform.position + transform.right, transform.localScale * .9f, 0, canCollideWith) == null && Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, ~cantCollideWith) != null) {
            GameObject flowing = Instantiate(gameObject, transform.position + transform.right, transform.rotation);
            flowing.GetComponent<block>().liquidLevel = liquidLevel - 1;
        }
        if(Physics2D.OverlapBox(transform.position - transform.right, transform.localScale * .9f, 0, canCollideWith) == null && Physics2D.OverlapBox(transform.position - transform.up, transform.localScale * .9f, 0, ~cantCollideWith) != null) {
            GameObject flowing = Instantiate(gameObject, transform.position - transform.right, transform.rotation);
            flowing.GetComponent<block>().liquidLevel = liquidLevel - 1;
        }

        if(Physics2D.OverlapBox(transform.position + transform.up, transform.localScale * .9f, 0, waterMask) != null) {
            liquidLevel = 7;
        }



    }
     void spawnItem(Item itemToSpawn) {
            GameObject spawnedItem = Instantiate(Resources.Load<GameObject>("Prefabs/DroppedItem"), transform.position, transform.rotation);
            spawnedItem.GetComponent<droppedItem>().itemId = itemToSpawn.id;
            spawnedItem.GetComponent<droppedItem>().itemAmount = itemToSpawn.amount;
    }

    void OnMouseOver() {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, originalColor + Color.white*.125f, .4f);
        if(GetComponent<Crafter>() != null) {
            return;
        }
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = $"Block: \n{breakProgress} / {breakTime}";
    }

    void OnMouseExit() {
        if(GetComponent<Crafter>() != null) {
            return;
        }
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = "";
    }

    public void damage(float miningPower, float breakForce) {
        if(miningPower >= minimumMiningPower) {
            breakProgress -= miningPower - minimumMiningPower + .5f;
            GetComponent<SpriteRenderer>().color = originalColor + Color.white;
            
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


}
