
using UnityEngine;
using UnityEditor;

public class BlockMaker : EditorWindow
{
    string blockName = "block";
    float blockBreakingTime = 7.5f;
    float blockMinimumMiningPower = 1;
    Sprite blockSprite;
    Color blockColor;
    
    
    
    [MenuItem("Window/Block creator")]
    public static void ShowWindow() {
        GetWindow<BlockMaker>("Block creator");
    }
    
    void OnGUI () {
        GUILayout.Label("Create a new block using the options below", EditorStyles.boldLabel);

        GUILayout.Label("Block stats", EditorStyles.boldLabel);
        blockName = EditorGUILayout.TextField("Block id", blockName);
        blockBreakingTime = EditorGUILayout.Slider("Block breaking time", blockBreakingTime, .1f, 50);
        blockMinimumMiningPower = EditorGUILayout.Slider("Block minimum mining power", blockMinimumMiningPower, 0, 50);
        
        EditorGUILayout.Space();
        
        GUILayout.Label("Block texture and color", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        blockSprite = (Sprite)EditorGUILayout.ObjectField("Block sprite", blockSprite, typeof(Sprite));
        blockColor = EditorGUILayout.ColorField("Block color", blockColor);
        EditorGUILayout.EndHorizontal();

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
        bBlock.breakTime = blockBreakingTime;
        bBlock.minimumMiningPower = blockMinimumMiningPower;
        s.color = blockColor;
        s.sprite = blockSprite;
        b.transform.position = Vector3.zero;
        Debug.Log("Block created at " + Vector3.zero);
    }
}
