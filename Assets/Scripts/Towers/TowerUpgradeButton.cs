using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeButton : MonoBehaviour
{
    public TowerType towerUpgrade;

    private NeededMaterialsUI neededMaterialsDisplay;


    private void Awake()
    {
        neededMaterialsDisplay = FindObjectOfType<NeededMaterialsUI>();
    }
    private void OnDestroy()
    {
        RemoveCostMaterialsUI();
    }

    public void RemoveCostMaterialsUI()
    {
        Debug.Log("click");
        neededMaterialsDisplay.RemoveCostMaterialsUI();
    }

    public void ShowCostMaterialsUI()
    {
        neededMaterialsDisplay.ShowCostMaterialsUI(towerUpgrade);
    }
    public void ShowReturnedMaterialsUI()
    {
        neededMaterialsDisplay.ShowReturnedMaterialsUI(towerUpgrade);        
    }
}
