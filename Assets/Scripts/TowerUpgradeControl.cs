using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TowerUpgradeControl : MonoBehaviour
{

    [SerializeField] TowerType towerType = TowerType.Plain;
    [SerializeField] GameObject[] UIGameObjects;
    [SerializeField] bool deactivateOnStart = true;

    BuildMaterialCollection buildMaterialCollection;
    TowerUpgradeCollection towerUpgrades;
    
    private void Awake()
    {
        buildMaterialCollection = GetComponent<BuildMaterialCollection>();
        towerUpgrades = GameObject.FindObjectOfType<TowerUpgradeCollection>();     
    }
    private void Start()
    {
        if (deactivateOnStart) ToggleUI(false);
  
    }
    public void ToggleUI(bool toggle)
    {
        foreach (GameObject go in UIGameObjects)
        {
            go.SetActive(toggle);
        }
    }
    
    public void SetUpgrade(TowerUpgradeButton button)
    {
        TowerType upgrade = button.towerUpgrade;

        bool canBuy = towerUpgrades.CanBuyUpgrade(upgrade);
        if (canBuy == false)
        {
            //FailUpdate();
            return;
        }
        towerUpgrades.BuyUpgrade(upgrade);

        GameObject upgradedTower = Instantiate(towerUpgrades.FindTowerByType(upgrade).GetTowerGameObject());
        upgradedTower.transform.parent = gameObject.transform.parent;
        upgradedTower.transform.localPosition = gameObject.transform.localPosition;
        upgradedTower.transform.localRotation = gameObject.transform.localRotation;
        upgradedTower.transform.localScale = gameObject.transform.localScale;
        upgradedTower.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex());
        Destroy(gameObject);
    }

    //private void FailUpdate()
    //{

    //}

}
