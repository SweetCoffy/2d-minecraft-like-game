using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public int index;
    RectTransform thisRect;
    Entity playerEntity;
    public Entity e;
    ItemManager im;
    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        playerEntity = GameObject.Find("Player").GetComponent<Entity>();
        thisRect = GetComponent<RectTransform>();
        if (e != null)
        {
            playerEntity = e;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerEntity)
        {
            playerEntity = GameObject.Find("Player").GetComponent<Entity>();
        }


        if (index > playerEntity.storedItems.Count - 1)
        {
            transform.GetChild(0).GetComponent<Text>().text = "";
            GetComponent<Image>().sprite = GameObject.Find("ItemManager").GetComponent<ItemManager>().blankTexture;
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
            return;
        }
        else
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        string amountString;
        int amount = playerEntity.storedItems[index].amount;

        if (amount > 1)
        {
            amountString = amount.ToString();
        }
        else
        {
            amountString = "";
        }

        transform.GetChild(0).GetComponent<Text>().text = amountString;
        GetComponent<Image>().sprite = GameObject.Find("ItemManager").GetComponent<ItemManager>().itemTextures[playerEntity.storedItems[index].id];
        if (playerEntity.lastitemUpdate == index)
        {
            playerEntity.lastitemUpdate = -1;
            thisRect.localScale = new Vector2(.6f, 1.6f);
        }
    }

    void FixedUpdate()
    {
        thisRect.localScale = Vector2.Lerp(thisRect.localScale, new Vector2(1, 1), .1f);
        if (playerEntity.GetSelectedItem() == index)
        {
            thisRect.localScale = Vector2.Lerp(thisRect.localScale, new Vector2(1.5f, 1.5f), .1f);

        }
    }




}
