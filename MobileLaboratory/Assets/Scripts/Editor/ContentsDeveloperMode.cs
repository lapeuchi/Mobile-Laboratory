using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MainScene))]
public class ContentsDeveloperMode : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        
        // MainScene generator = (MainScene)target;
        // if (GUILayout.Button("Generate Cubes"))
        // {
        //     generator.GenerateCubes();
        // }
    }
}
