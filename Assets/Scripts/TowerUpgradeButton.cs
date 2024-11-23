using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeButton : MonoBehaviour
{
    public TowerType towerUpgrade;

    [Header("Material UI")]
    [SerializeField] private string neededMaterialsDisplayName;
    [SerializeField] private GameObject materialCountPrefab;
    [SerializeField] private GameObject notEnoughMaterialCountPrefab;

    private GameObject neededMaterialsDisplay;
    TowerUpgradeCollection towerUpgrades;
    private BuildMaterialCollection buildMaterialCollection;

    private void Awake()
    {
        buildMaterialCollection = FindAnyObjectByType<BuildMaterialCollection>();
        towerUpgrades = FindAnyObjectByType<TowerUpgradeCollection>();
    }
    private void Start()
    {
        HorizontalLayoutGroup[] layoutGroups = FindObjectsOfType<HorizontalLayoutGroup>();
        foreach (HorizontalLayoutGroup layoutGroup in layoutGroups)
        {
            if(layoutGroup.gameObject.name == neededMaterialsDisplayName)
            {
                neededMaterialsDisplay = layoutGroup.gameObject;
            }
        }
    }
    private void OnDestroy()
    {
        RemoveCostMaterilsUI();
    }
    public void ShowCostMaterialsUI()
    {
       
        MaterialCost[] materialCost = towerUpgrades.FindTowerByType(towerUpgrade).GetMaterialCosts();
        foreach(MaterialCost material in materialCost) {
            BuildMaterial buildMaterial = material.GetBuildMaterial();
            MaterialUse materialUse = buildMaterialCollection.GetMaterialUse(buildMaterial);

            GameObject materialCountInstance = material.GetCost() <= materialUse.GetCount() ? 
                Instantiate(materialCountPrefab, neededMaterialsDisplay.transform) : 
                Instantiate(notEnoughMaterialCountPrefab, neededMaterialsDisplay.transform);
           
            MaterialCountUI materialCountUI = materialCountInstance.GetComponent<MaterialCountUI>();
            materialCountUI.SetMaterial(materialUse.GetBuildMaterialSO().GetSprite(), material.GetCost());
            

        }
    }
    public void RemoveCostMaterilsUI()
    {
        Transform[] children = neededMaterialsDisplay.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            GameObject gameObject = children[i].gameObject;
            if(gameObject != neededMaterialsDisplay)
            {
                Destroy(gameObject);
            }
        }
    }
}
