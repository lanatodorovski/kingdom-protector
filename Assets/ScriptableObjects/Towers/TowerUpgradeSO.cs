using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgrade", menuName = "ScriptableObjects/TowerUpgradeSO")]
public class TowerUpgradeSO : ScriptableObject 
{
    [SerializeField] TowerType type;
    [SerializeField] GameObject obj;
    [SerializeField] MaterialCount[] materialCosts;

    public TowerType GetTowerType()
    {
        return type;
    }
    public GameObject GetTowerGameObject()
    {
        return obj;
    }
    public MaterialCount[] GetMaterialCosts()
    {
        return (MaterialCount[])materialCosts.Clone();
    }

}
[Serializable]
public enum TowerType
{
    Plain,
    Basic,
    Upgrade_1
}
[Serializable] //MaterialCount is used to connect a count or cost with a certain BuildMaterial
public class MaterialCount
{
    [SerializeField] BuildMaterial material;
    [SerializeField] int count;

    public MaterialCount() { }
    public MaterialCount(BuildMaterial material, int cost) { 
        this.material = material;
        this.count = cost;
    }
    public BuildMaterial GetBuildMaterial()
    {
        return material;
    }
    public int GetCount()
    {
        return count;
    }
    public void AddCount(int addCount)
    {
        count += addCount;
    }
}