using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using Unity.VisualScripting;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator) target;

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();

            MapBoundsControl boundsControl = target.GetComponent<MapBoundsControl>();
            boundsControl.SetBounds();

        }

        DrawDefaultInspector();
    }

}
