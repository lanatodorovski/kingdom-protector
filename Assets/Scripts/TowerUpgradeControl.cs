using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TowerUpgradeControl : MonoBehaviour
{
    public static List<TowerUpgradeControl> towerScripts = new List<TowerUpgradeControl>();        
    [SerializeField] TowerType towerType = TowerType.Plain;
    [SerializeField] GameObject[] UIGameObjects;
    [SerializeField] bool deactivateOnStart = true;
   
    TowerUpgradeCollection towerUpgrades;    
    
    private void Awake()
    {   
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
    
    public void SetUpgrade(TowerType upgrade, UpgradeMode upgradeMode = UpgradeMode.None)
    {        
        if (upgradeMode != UpgradeMode.None)
        {
            if (upgradeMode == UpgradeMode.Return)
            {
                towerUpgrades.BuyUpgrade(upgrade, true);
                upgrade = TowerType.Plain;
            }
            else
            {
                bool canBuy = towerUpgrades.CanBuyUpgrade(upgrade);
                if (canBuy == false)
                {
                    return;
                }
                towerUpgrades.BuyUpgrade(upgrade);
            }
        }        
        GameObject upgradedTower = Instantiate(towerUpgrades.FindTowerByType(upgrade).GetTowerGameObject()) ;
        upgradedTower.transform.parent = gameObject.transform.parent;
        upgradedTower.transform.localPosition = gameObject.transform.localPosition;
        upgradedTower.transform.localRotation = gameObject.transform.localRotation;
        upgradedTower.transform.localScale = gameObject.transform.localScale;
        upgradedTower.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex());   

        int thisGOindex = towerScripts.IndexOf(this);
        towerScripts[thisGOindex] = upgradedTower.GetComponent<TowerUpgradeControl>();

        Destroy(gameObject);
    }
    public void SetUpgrade(TowerUpgradeButton button)
    {
        SetUpgrade(button.towerUpgrade, UpgradeMode.Buy);
    }
    public void SetUpgradeAndReturnMaterials(TowerUpgradeButton button)
    {
        SetUpgrade(button.towerUpgrade, UpgradeMode.Return);
    }

    public TowerType GetTowerType()
    {
        return towerType;
    }
    public static void SetTowerScripts()
    {
        towerScripts.Clear();
        towerScripts.AddRange(
            GameObject.FindGameObjectWithTag("Tower").transform.parent.GetComponentsInChildren<TowerUpgradeControl>()
        );
    }    
    public enum UpgradeMode
    {
        Buy,
        Return,
        None
    }
}
