using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildMaterialCollection : MonoBehaviour
{

    [SerializeField]
    BuildMaterialSO[] materialItems;
    public BuildMaterialSO GetMaterial(BuildMaterial material)
    {
        foreach(BuildMaterialSO materialItem in materialItems)
        {
            if(materialItem.GetBuildMaterial() == material)
            {
                return materialItem;
            }
        }
        return null;
    }
    public int GetCountByMaterial(BuildMaterial material)
    {
        BuildMaterialSO materialItem = GetMaterial(material);
        return materialItem.GetCount();
    }
}

