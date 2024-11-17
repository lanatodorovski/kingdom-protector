using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class TowerUpgrades : MonoBehaviour
{

    [SerializeField]
    TowerUpgradesItem[] towers;

    GameManager gameManager;
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    public TowerUpgradesItem FindTowerByType(TowerType type)
    {
        foreach (TowerUpgradesItem item in towers)
        {
            if (item.GetTowerType().Equals(type))
            {
                return item;
            }
        }
        return null;
    }
    public bool CanBuyUpgrade(TowerType upgrade)
    {
        TowerUpgradesItem towerUpgrade = FindTowerByType(upgrade);
        foreach (MaterialCost costMaterial in towerUpgrade.GetMaterialCosts())
        {
            int materialCount = gameManager.GetCountByMaterial(costMaterial.GetBuildMaterial());
            if(materialCount < costMaterial.GetCost())
            {
                return false;
            }
        }
        return true;
    }
    public void BuyUpgrade(TowerType upgrade)
    {
        TowerUpgradesItem towerUpgrade = FindTowerByType(upgrade);
        foreach (MaterialCost costMaterial in towerUpgrade.GetMaterialCosts())
        {
            BuildMaterialItem materialCount = gameManager.GetMaterial(costMaterial.GetBuildMaterial());
            materialCount.CostTakeAway(costMaterial.GetCost());
        }
    }
}


[Serializable]
public enum TowerType
{
    Plain,
    Basic
}

[Serializable]
public class TowerUpgradesItem
{
    
    [SerializeField]TowerType type;
    [SerializeField]GameObject obj;
    [SerializeField] MaterialCost[] materialCosts;

    public TowerType GetTowerType()
    {
        return type;
    }
    public GameObject GetTowerGameObject()
    {
        return obj;
    }
    public MaterialCost[] GetMaterialCosts()
    {
        return (MaterialCost[]) materialCosts.Clone();
    }
}

[Serializable]
public class MaterialCost
{
    [SerializeField] BuildMaterial material;
    [SerializeField] int cost;

    public BuildMaterial GetBuildMaterial()
    {
        return material;
    }
    public int GetCost() { 
        return cost;
    }
}