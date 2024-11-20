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
    public void ShowCostMaterialsUI()
    {
       
        MaterialCost[] materialCost = towerUpgrades.FindTowerByType(towerUpgrade).GetMaterialCosts();
        foreach(MaterialCost material in materialCost) {
            BuildMaterial buildMaterial = material.GetBuildMaterial();
            BuildMaterialSO buildMaterialSO = buildMaterialCollection.GetMaterial(buildMaterial);

            GameObject materialCountInstance = Instantiate(materialCountPrefab, neededMaterialsDisplay.transform);
            MaterialCountUI materialCountUI = materialCountInstance.GetComponent<MaterialCountUI>();
            materialCountUI.SetBuildMaterial(buildMaterialSO.GetSprite(), material.GetCost());
            

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
