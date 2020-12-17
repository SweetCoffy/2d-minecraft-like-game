using System.Collections.Generic;

// to do: actually finish this thing
[System.Serializable]
public static class SaveData
{
    public static PlayerData playerData;
    public static BlockData[] blockDataArray;
    public static BlockData[] GenerateBlockDataArray()
    {
        blockDataArray = new BlockData[blockDataList.Count];
        for (int i = 0; i < blockDataArray.Length; i++)
        {
            blockDataArray[i] = blockDataList[i];
        }
        return blockDataArray;
    }
    [System.NonSerialized]
    public static List<BlockData> blockDataList;
}
[System.Serializable]
public class PlayerData
{
    public float maxHealth;
    public float maxThirst;
    public float maxMana;
    public Item[] items;
    public PlayerData(Entity player)
    {
        Save(player);
    }
    public void Save(Entity e)
    {
        items = new Item[e.storedItems.Count];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = e.storedItems[i];
        }
        maxHealth = e.maxHealth;
        maxThirst = e.maxThirst;
        maxMana = e.maxMana;
    }
}
[System.Serializable]
public class BlockData
{
    public int id = 0;
    public int liquidLevel = 8;
    public float x;
    public float y;
    public BlockData(Block b)
    {
        Save(b);
    }
    public void Save(Block b)
    {
        id = b.id;
        liquidLevel = b.liquidLevel;
        y = b.transform.position.y;
        x = b.transform.position.x;
    }
}