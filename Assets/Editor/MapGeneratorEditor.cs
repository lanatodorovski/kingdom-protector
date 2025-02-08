using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator) target;

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();

        }

        DrawDefaultInspector();
    }

}
