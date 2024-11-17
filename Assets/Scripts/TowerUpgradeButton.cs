using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeButton : MonoBehaviour
{
    public TowerType towerUpgrade;

    [Header("Material UI")]
    [SerializeField] private string neededMaterialsDisplayName;
    [SerializeField] private GameObject materialCountGO;

    private GameObject neededMaterialsDisplay;
    TowerUpgrades towerUpgrades;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        towerUpgrades = FindAnyObjectByType<TowerUpgrades>();
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
            BuildMaterialItem buildMaterialItem = gameManager.GetMaterial(buildMaterial);

            GameObject materialCountInstance = Instantiate(materialCountGO, neededMaterialsDisplay.transform);
            MaterialCountUI materialCountUI = materialCountGO.GetComponent<MaterialCountUI>();
            materialCountUI.SetMaterialUI(buildMaterialItem.GetSprite(), material.GetCost());
            

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
