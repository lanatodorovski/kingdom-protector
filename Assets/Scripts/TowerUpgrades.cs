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
    public TowerUpgradesDictionary towerUpgradesDictionary;


}


[Serializable]
public enum TowerType
{
    Plain,
    Basic
}

[Serializable]
public class TowerUpgradesDictionary {
    [SerializeField]
    TowerUpgradesItem[] towers;

    public GameObject findTowerByType(TowerType type)
    {
        foreach(TowerUpgradesItem item in towers)
        {
            if(item.GetTowerType().Equals(type))
            {
                return item.GetTowerGameObject();
            }
        }
        return null;
    }
}
[Serializable]
public class TowerUpgradesItem
{
    
    [SerializeField]TowerType type;
    [SerializeField]GameObject obj;

    public TowerType GetTowerType()
    {
        return type;
    }
    public GameObject GetTowerGameObject()
    {
        return obj;
    }
}