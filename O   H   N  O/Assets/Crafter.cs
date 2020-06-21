using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(block))]
public class Crafter : MonoBehaviour
{
    public int[] input;
    public int[] inputAmountNeeded;
    public int[] output;
    public int[] outputAmount;
    public float craftingTime;
    int[] inputItems;
    itemManager im;
    public string crafterName;
    float[] craftingProgress;
    public string getDisplayText() {
        string currText = $"{crafterName}: ";

        for(int i = 0; i < output.Length; i++) {

            currText = currText + $"\n{im.itemNames[input[i]]}: {inputItems[i]} / {inputAmountNeeded[i]} \n{Mathf.Round(getProgressNormalized(i))}%\n{outputAmount[i]}x {im.itemNames[output[i]]}\n";
        
        }
    
    
    return currText;
    }
    public float getProgressNormalized(int index = 0, bool percent = true) {
        
        if(percent) {
            return craftingProgress[index]/craftingTime * 100;
        } else {
            return craftingProgress[index]/craftingTime;
        }
    }

    GameObject itemPrefab;
    public block b;

    void Start() {
        inputItems = new int[output.Length];
        craftingProgress = new float[output.Length];
        
        
        im = GameObject.Find("ItemManager").GetComponent<itemManager>();
        itemPrefab = Resources.Load<GameObject>("Prefabs/DroppedItem");
        b = GetComponent<block>();
    }
    
    
    void Update() {
        int i = 0;
        foreach(int o in output) {
        if(i > output.Length-1) {
            break;
        }
        if(inputItems[i] >= inputAmountNeeded[i]) {
            craftingProgress[i] += Time.deltaTime;
            if(craftingProgress[i] >= craftingTime) {
                GameObject outputItem = Instantiate(itemPrefab, transform.position, transform.rotation);
                outputItem.GetComponent<droppedItem>().itemId = o;
                outputItem.GetComponent<droppedItem>().itemAmount = outputAmount[i];
                craftingProgress[i] = 0;
                inputItems[i] -= inputAmountNeeded[i];
            }
        }
        i++;
        }

    }
    
    void OnMouseDown() {
        entity e = GameObject.Find("Player").GetComponent<entity>();
        if(e.getSelectedItem() > e.storedItems.Count -1) {
            return;
        }
        int i = 0;
        foreach(int currentInput in input) {
        if(e.storedItems[e.getSelectedItem()].id == input[i]) {
            inputItems[i] += 1;
            e.consumeItem(e.getSelectedItem());
        }
        i++;
        }
        

    }

    void OnMouseOver() {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = getDisplayText();
    }

    void OnMouseExit() {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = "";
    }

    void OnCollisionStay2D(Collision2D col) {
        GameObject other = col.gameObject;
        int ii = 0;
        if(other.GetComponent<droppedItem>() != null) {
            droppedItem i = other.GetComponent<droppedItem>();
            foreach(int inputItem in input) {
            if(i.itemId == inputItem) {
                inputItems[ii] += i.itemAmount;
                Destroy(other);
            }
            ii++;
            }
            

        }
    }


}
