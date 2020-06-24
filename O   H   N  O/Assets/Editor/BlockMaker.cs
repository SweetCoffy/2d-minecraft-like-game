
using UnityEngine;
using UnityEditor;

public class BlockMaker : EditorWindow {
    string blockName = "block";
    float blockBreakingTime = 7.5f;
    float blockMinimumMiningPower = 1;
    Sprite blockSprite;
    Color blockColor = Color.white;
    CraftingRecipe recipe;
    float smelterTier = 1;
    bool addSmelter;
    bool folded;
    bool enableDisable;
    public static bool cancelIfRecipeIsNull = true;
    
    
    
    [MenuItem("Tools/Sebo2205's stuff/Block Creator")]
    public static void ShowWindow() {
        GetWindow<BlockMaker>("Block creator");
    }    
    void OnGUI () {
        GUILayout.Label("Create a new block using the options below", EditorStyles.boldLabel);

        GUILayout.Label("Block Stats", EditorStyles.boldLabel);
        blockName = EditorGUILayout.TextField("Block ID", blockName);
        blockBreakingTime = EditorGUILayout.Slider("Block Breaking Time", blockBreakingTime, .1f, 50);
        blockMinimumMiningPower = EditorGUILayout.Slider("Block Minimum Mining Power", blockMinimumMiningPower, 0, 50);
        
        
        EditorGUILayout.Space();
        
        GUILayout.Label("Block Texture and Color", EditorStyles.boldLabel);
        blockSprite = (Sprite)EditorGUILayout.ObjectField("Block Sprite", blockSprite, typeof(Sprite));
        blockColor = EditorGUILayout.ColorField("Block Color", blockColor);

        addSmelter = EditorGUILayout.BeginToggleGroup(new GUIContent("Smelter", "A Smelter component should be added to the block?"), addSmelter);
           recipe = (CraftingRecipe)EditorGUILayout.ObjectField("Smelter Recipe", recipe, typeof(CraftingRecipe)); 
           smelterTier = EditorGUILayout.Slider("Smelter Tier", smelterTier, .1f, 10);
        EditorGUILayout.EndToggleGroup();
        
        
        if(GUILayout.Button("Create block")) {
            NewBlock();
        }
    }

    void NewBlock() {
        
        GameObject b = new GameObject(blockName);
        block bBlock = b.AddComponent<block>();
        SpriteRenderer s = b.AddComponent<SpriteRenderer>();
        b.AddComponent<snap>();
        b.AddComponent<BoxCollider2D>();
        if(addSmelter) {
            Smelter smelt = b.AddComponent<Smelter>();
            smelt.recipe = recipe;
            smelt.smelterTier = smelterTier;
            if(recipe == null && cancelIfRecipeIsNull) {
                Destroy(b);
                Debug.LogError("Error!, can't spawn the object because the smelter recipe is null, change it and try again");
                return;
            }
        }
        
        bBlock.breakTime = blockBreakingTime;
        bBlock.minimumMiningPower = blockMinimumMiningPower;
        s.color = blockColor;
        s.sprite = blockSprite;
        b.transform.position = Vector3.zero;
        Debug.Log("Block created at " + Vector3.zero);
    }
}
