using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class TowerUpgradeControl : MonoBehaviour
{

    [SerializeField] TowerType towerType = TowerType.Plain;
    [SerializeField]GameObject canvasGameObject;
    [SerializeField] bool deactivateOnStart = true;
    TowerUpgradesDictionary towerUpgradesDictionary;
    private void Awake()
    {
        towerUpgradesDictionary = GameObject.FindObjectOfType<TowerUpgrades>().towerUpgradesDictionary;     
    }
    private void Start()
    {
        if(deactivateOnStart) canvasGameObject.SetActive(false);
    }
    public void ActivateUI()
    {
        canvasGameObject.SetActive(true);
    }
    
    public void SetUpgrade(TowerUpgradeButton button)
    {
        TowerType upgrade = button.towerUpgrade;

        GameObject upgradedTower = Instantiate(towerUpgradesDictionary.findTowerByType(upgrade));
        upgradedTower.transform.parent = gameObject.transform.parent;
        upgradedTower.transform.localPosition = gameObject.transform.localPosition;
        upgradedTower.transform.localRotation = gameObject.transform.localRotation;
        upgradedTower.transform.localScale = gameObject.transform.localScale;
        upgradedTower.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex());
        Destroy(gameObject);
    }
}
