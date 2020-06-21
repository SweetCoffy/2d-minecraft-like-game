using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryItem : MonoBehaviour
{
    public int index;
    public Vector2 originalPosition;
    RectTransform thisRect;
    entity playerEntity;
    itemManager im;
    // Start is called before the first frame update
    void Start()
    {
        im = GameObject.Find("ItemManager").GetComponent<itemManager>();
        playerEntity = GameObject.Find("Player").GetComponent<entity>();
        thisRect = GetComponent<RectTransform>();
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(index > GameObject.Find("Player").GetComponent<entity>().storedItems.Count - 1) {
            transform.GetChild(0).GetComponent<Text>().text = "";
            GetComponent<Image>().sprite = GameObject.Find("ItemManager").GetComponent<itemManager>().blankTexture;
            return;
        }
        transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("Player").GetComponent<entity>().storedItems[index].amount.ToString() + "x";
        GetComponent<Image>().sprite = GameObject.Find("ItemManager").GetComponent<itemManager>().itemTextures[GameObject.Find("Player").GetComponent<entity>().storedItems[index].id];
        if(playerEntity.lastitemUpdate == index) {
          playerEntity.lastitemUpdate = -1;
          thisRect.localScale = new Vector2(.6f, 1.6f);
        }
    }

    void FixedUpdate() {
        thisRect.anchoredPosition = Vector2.Lerp(thisRect.anchoredPosition, originalPosition, .2f);
        thisRect.localScale = Vector2.Lerp(thisRect.localScale, new Vector2(1, 1), .25f);
        if(playerEntity.getSelectedItem() == index) {
            thisRect.anchoredPosition = Vector2.Lerp(thisRect.anchoredPosition, originalPosition + new Vector2(0, 5f), .2f);
            thisRect.localScale = Vector2.Lerp(thisRect.localScale, new Vector2(1.5f, 1.5f), .2f);

        }
    }




}
