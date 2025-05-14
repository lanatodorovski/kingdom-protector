using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class TowerUpgradeCollection : MonoBehaviour
{

    [SerializeField]
    TowerUpgradeSO[] towers;

    BuildMaterialCollection buildMaterialCollection;
    private void Awake()
    {
        buildMaterialCollection = GetComponent<BuildMaterialCollection>();
    }
    public TowerUpgradeSO FindTowerByType(TowerType type)
    {
        foreach (TowerUpgradeSO item in towers)
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
        TowerUpgradeSO towerUpgrade = FindTowerByType(upgrade);
        foreach (MaterialCount costMaterial in towerUpgrade.GetMaterialCosts())
        {
            int materialCount = buildMaterialCollection.GetCountByMaterial(costMaterial.GetBuildMaterial());
            if(materialCount < costMaterial.GetCount())
            {
                return false;
            }
        }
        return true;
    }
    public void BuyUpgrade(TowerType upgrade, bool returnMaterials = false)
    {
        TowerUpgradeSO towerUpgrade = FindTowerByType(upgrade);
        foreach (MaterialCount costMaterial in towerUpgrade.GetMaterialCosts())
        {
            MaterialUse materialCount = buildMaterialCollection.GetMaterialUse(costMaterial.GetBuildMaterial());
            if (!returnMaterials)
            {
                materialCount.TakeAwayCount(costMaterial.GetCount());
            }
            else
            {
                materialCount.AddCount(costMaterial.GetCount());
            }
        }
    }    

}



