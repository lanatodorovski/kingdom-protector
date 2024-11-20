using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgrade", menuName = "TowerUpgradeSO")]
public class TowerUpgradeSO : ScriptableObject 
{
    [SerializeField] TowerType type;
    [SerializeField] GameObject obj;
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
        return (MaterialCost[])materialCosts.Clone();
    }

}
[Serializable]
public enum TowerType
{
    Plain,
    Basic
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
    public int GetCost()
    {
        return cost;
    }
}
