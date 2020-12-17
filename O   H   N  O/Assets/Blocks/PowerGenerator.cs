using UnityEngine;

// Base of all power generators
public class PowerGenerator : PowerBlock
{
    // The base power generation, can be accessed by the classes that extend it
    public float basePowerGeneration;

    // Multiplies the base power generation to get the power generation
    protected float powerGeneration = 0;

    // Maximum power generation that can be achieved with this Block
    public float maxPowerGeneration;



    // The current power generation
    protected float currentPowerGeneration;

    protected override void Update()
    {
        base.Update();
        // Update the current power generation
        currentPowerGeneration = Mathf.Clamp(basePowerGeneration * powerGeneration, 0, maxPowerGeneration);
        // Update the power consumption
        powerConsume = -currentPowerGeneration;
    }



}