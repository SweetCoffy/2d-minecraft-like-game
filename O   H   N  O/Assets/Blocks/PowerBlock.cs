using UnityEngine;
using UnityEngine.UI;

public class PowerBlock : Block
{

    // Variables
    public float powerCapacity; // How much power the Block can hold
    public float storedPower; // How much power the Block currently has
    // How much power this blocks consumes per second, negative values can be used to generate power instead of consuming
    protected float powerConsume = 0;

    public bool outputsPower = true;

    public bool receivesPower = false;

    public float powerTransferRate = 10;

    // Whether or not this Block can consume power, useful for blocks that only consume power sometimes
    public bool consumePower;

    // Whether or not the power requirements are met, useful for making blocks that require power to work
    public bool requirementsMet;
    protected override void Update()
    {
        base.Update();
        // Consumes power if the Block consumes any
        if (powerConsume != 0)
        {
            storedPower = Mathf.Clamp(storedPower - (powerConsume * Time.deltaTime), 0, powerCapacity);
        }
        if (storedPower - (powerConsume * Time.deltaTime) >= 0)
        {
            requirementsMet = true;
        }
        else
        {
            requirementsMet = false;
        }
    }

    public float GetConsuming()
    {
        if (consumePower)
        {
            return -powerConsume;
        }
        else
        {
            return 0;
        }
    }

    public override void ShowInfo()
    {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = $"Block: \n{breakProgress} / {breakTime} \nPower: \n{storedPower.ToString("#######0.0")} / {powerCapacity.ToString("######0.0")} Power Units \n{GetConsuming().ToString("<color=lime>+####0.0</color>;<color=red>-####0.0</color>")} PU/s";
    }

    // Now the code won't incinerate power, instead it will refuse to transfer the power if the target Block is full
    void OnCollisionStay2D(Collision2D col)
    {
        PowerBlock power = col.gameObject.GetComponent<PowerBlock>();
        if (power != null)
        {
            float transferAmount = Mathf.Clamp(powerTransferRate * Time.deltaTime, 0, storedPower);
            float otherTransferAmount = Mathf.Clamp(power.powerTransferRate * Time.deltaTime, 0, power.storedPower);

            if (power.storedPower + transferAmount > power.powerCapacity)
            {
                return;
            }
            if (outputsPower && power.receivesPower)
            {
                storedPower -= transferAmount;
                power.storedPower += transferAmount;
            }



        }
    }
}
