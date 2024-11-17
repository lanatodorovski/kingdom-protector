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
            material.SetCountUI();
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
    [SerializeField]private BuildMaterial material;
    [SerializeField]private int count;
    [SerializeField]private Sprite sprite;
    [SerializeField]private TextMeshProUGUI materialCountUI;

    public void SetCountUI()
    {
        this.materialCountUI.text = this.count.ToString();  
    }
    public BuildMaterial GetBuildMaterial()
    {
        return this.material;
    }
    public int GetCount()
    {
        return count;
    }
    public void CostTakeAway(int cost)
    {
        count -= cost;
        Debug.Log(count);
        this.SetCountUI();
    }
}

[Serializable]
public enum BuildMaterial{ 
    Wood,
    Stone 
}
