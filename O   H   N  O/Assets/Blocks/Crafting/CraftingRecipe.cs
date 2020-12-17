using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public Item[] input;
    public Item output;
    public float craftDuration = 1;
}
