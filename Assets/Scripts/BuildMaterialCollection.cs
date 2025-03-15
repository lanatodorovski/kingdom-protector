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

    [SerializeField] private bool loadFromLocalSave;
    public void Start()
    {
        if(FindAnyObjectByType<LocalSaveSystem>() != null && loadFromLocalSave)      
            LoadLocalMaterials();
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
        foreach (MaterialUse materialUse in materialUses)
        {
            materialUse.GetMaterialCountUI().SetMaterial(materialUse);
        }
    }

    public MaterialUse[] GetMaterialUses()
    {
        return materialUses;
    }

    public List<MaterialCount> GetAllAsMaterialCount()
    {
        List<MaterialCount> materialCounts = new List<MaterialCount>();
        foreach (MaterialUse materialUse in materialUses)
        {
            materialCounts.Add(new MaterialCount(materialUse.GetBuildMaterialSO().GetBuildMaterial(), materialUse.GetCount()));
        }
        return materialCounts;
    }

    private void LoadLocalMaterials()
    {
        List<MaterialCount> savedMaterials = FindAnyObjectByType<LocalSaveSystem>().LoadSave(0).materialCount;
        foreach (MaterialCount savedMaterial in savedMaterials)
        {
            MaterialUse materialUse = Array.Find(materialUses, material => material.GetBuildMaterialSO().GetBuildMaterial() == savedMaterial.GetBuildMaterial());
            materialUse.SetCount(savedMaterial.GetCount());
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
    public void SetCount(int newCount)
    {
        this.count = newCount;
    }
    public MaterialCountUI GetMaterialCountUI()
    {
        return materialCountUI;
    }
    public void TakeAwayCount(int removeCount)
    {
        count -= removeCount;
        materialCountUI.SetCountUI(count);
        if (materialCountUI.txtMaterialAdd != null) materialCountUI.ShowAddedMaterial(-removeCount);
    }
    public void AddCount(int addCount){ 
        count += addCount;
        materialCountUI.SetCountUI(count);
        if (materialCountUI.txtMaterialAdd != null) materialCountUI.ShowAddedMaterial(addCount); 
    }
}