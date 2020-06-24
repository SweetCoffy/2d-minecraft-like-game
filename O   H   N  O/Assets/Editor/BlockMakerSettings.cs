using UnityEngine;
using UnityEditor;

public class BlockMakerSettings : EditorWindow
{
    [MenuItem("Tools/Sebo2205's stuff/Block Creator Settings")]
    public static void ShowWindow() {
        GetWindow<BlockMakerSettings>("Block Creator Settings");
    }
    void OnGUI() {
        GUILayout.Label("Block Creator Settings");

        BlockMaker.cancelIfRecipeIsNull = EditorGUILayout.Toggle(
            "Cancel if the recipe is null", 
            BlockMaker.cancelIfRecipeIsNull);
    }
}
