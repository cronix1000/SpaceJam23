using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetupMap))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SetupMap mapGen = (SetupMap)target;

        //if (DrawDefaultInspector())
        //{
        //    if (mapGen.autoUpdate)
        //        mapGen.DrawMapInEditor();
        //}

        //if (GUILayout.Button("Generate"))
        //{
        //    mapGen.DrawMapInEditor();
        //}
    }
}