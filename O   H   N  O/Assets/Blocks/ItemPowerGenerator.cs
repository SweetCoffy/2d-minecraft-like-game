using UnityEngine;
using UnityEngine.UI;

// Power generator that generates power using an item
public class ItemPowerGenerator : PowerGenerator {
    // How much fuel items can this Block hold
    public int itemCapacity;
    
    // The fuel item id, used for fuel acceptance
    public int fuelItemId;

    // How much fuel items is this Block holding
    protected int storedFuel = 0;
    
    // How much time a fuel item lasts
    public float fuelDuration;

    // For how much time the fuel item is being used
    protected float fuelUsage = 0; 
    
    public override void Update() {
        base.Update();
        if(storedFuel > 0) {
            if(fuelUsage < fuelDuration) {
                fuelUsage += Time.deltaTime;
            } else {
                fuelUsage = 0;
                storedFuel--;
            }
            powerGeneration = 1;
        } else {
            powerGeneration = 0;
        }
    }

    public override void ShowInfo() {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = $"Block: \n{breakProgress} / {breakTime} \nPower: \n{storedPower.ToString("#######0.0")} / {powerCapacity.ToString("######0.0")} Power Units \n{(basePowerGeneration * powerGeneration).ToString("<color=lime>+####0.0</color>")} PU/s \n{storedFuel}/{itemCapacity} Fuel";
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(storedFuel >= itemCapacity) {
            return;
        }
        DroppedItem it = col.gameObject.GetComponent<DroppedItem>();
        if(it != null) {
            if(it.itemId == fuelItemId) {
                storedFuel += it.itemAmount;
                Destroy(it.gameObject);
            }
        }
    }
}