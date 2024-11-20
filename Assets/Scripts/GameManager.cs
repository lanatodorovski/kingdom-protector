using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameState gameState = GameState.Build;

    [SerializeField]
    BuildMaterialItem[] materialItems;

    private void Start()
    {
        setupMaterialUI();
    }

    private void setupMaterialUI()
    {
        for (int i = 0; i < materialItems.Length; i++)//BuildMaterial material in materials
        {
            BuildMaterialItem material = materialItems[i];

            MaterialCountUI materialCountUI = material.GetMaterialCountUI();                
            materialCountUI.SetMaterialUI(material.GetSprite(), material.GetCount());
        }
    }
    public BuildMaterialItem GetMaterial(BuildMaterial material)
    {
        foreach(BuildMaterialItem materialItem in materialItems)
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
        BuildMaterialItem materialItem = GetMaterial(material);
        return materialItem.GetCount();
    }
}

[Serializable]
public enum GameState
{
    Battle,
    Build,
    Scout
}
[Serializable]
public class BuildMaterialItem
{
    [SerializeField] private BuildMaterial material;
    [SerializeField] private int count;
    [SerializeField] private Sprite sprite;
    [SerializeField] private MaterialCountUI materialCountUI;

    public MaterialCountUI GetMaterialCountUI()
    {
        Debug.Log(count);
        return this.materialCountUI;
    }
    public BuildMaterial GetBuildMaterial()
    {
        return this.material;
    }
    public int GetCount()
    {
        return count;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    public void CostTakeAway(int cost)
    {
        count -= cost;        
        this.materialCountUI.SetCountUI(count);
    }
}

[Serializable]
public enum BuildMaterial{ 
    Wood,
    Stone 
}
