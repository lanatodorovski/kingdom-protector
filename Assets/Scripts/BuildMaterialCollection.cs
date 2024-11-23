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
    MaterialUse[] materialUses;


    public void Start()
    {
        MaterialCountUISetup();
    }
    public MaterialUse GetMaterialUse(BuildMaterial material)
    {
        foreach (MaterialUse materialCount in materialUses)
        {
            if (materialCount.GetBuildMaterialSO().GetBuildMaterial().Equals(material))
            {
                return materialCount;
            }
        }
        return null;
    }
    public BuildMaterialSO GetMaterialSO(BuildMaterial material)
    {
        MaterialUse materialUse = GetMaterialUse(material);
        return materialUse.GetBuildMaterialSO();
    }
    public int GetCountByMaterial(BuildMaterial material)
    {
        MaterialUse materialUse = GetMaterialUse(material);
        return materialUse.GetCount();
    }

    private void MaterialCountUISetup()
    {
        foreach(MaterialUse materialUse in materialUses)
        {
            materialUse.GetMaterialCountUI().SetMaterial(materialUse);
        }
    }
}


[Serializable]
public class MaterialUse
{
    [SerializeField] BuildMaterialSO material;
    [SerializeField] int count;
    [SerializeField] MaterialCountUI materialCountUI;

    public BuildMaterialSO GetBuildMaterialSO()
    {
        return material;
    }
    public int GetCount()
    {
        return count;
    }
    public MaterialCountUI GetMaterialCountUI()
    {
        return materialCountUI;
    }
    public void TakeAwayCount(int removeCount)
    {
        count -= removeCount;
        materialCountUI.SetCountUI(count);
    }
    public void AddCount(int addCount){ 
        count += addCount;
        materialCountUI.SetCountUI(count);
    }
}