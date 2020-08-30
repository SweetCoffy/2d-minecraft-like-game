using UnityEngine;
using UnityEngine.UI;

// A power generator that works at a random capacity and generates free power
public class RandomGenerator : PowerGenerator {
    public float maxProduction = 1.5f;
    public float minProduction = 0.75f;
    public float lerpSpeed = 0.1f;
    
    void Update() {
        powerGeneration = Mathf.Lerp(powerGeneration, Random.Range(minProduction, maxProduction), lerpSpeed);
        UpdateConsume();
    }

    public override void ShowInfo() {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>().text = $"Block: \n{breakProgress} / {breakTime} \nPower: \n{storedPower.ToString("#######0.0")} / {powerCapacity.ToString("######0.0")} Power Units \n{(basePowerGeneration * powerGeneration).ToString("<color=lime>+####0.0</color>")} PU/s";
    }
}