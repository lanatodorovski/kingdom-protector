using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeededMaterialsUI : MonoBehaviour
{

    [SerializeField] private GameObject materialCountPrefab;
    [SerializeField] private GameObject notEnoughMaterialCountPrefab;

    TowerUpgradeCollection towerUpgrades;
    private BuildMaterialCollection buildMaterialCollection;
    private void Awake()
    {
        buildMaterialCollection = FindAnyObjectByType<BuildMaterialCollection>();
        towerUpgrades = FindAnyObjectByType<TowerUpgradeCollection>();
    }
    public void ShowCostMaterialsUI(TowerType towerUpgrade)
    {
        if(gameObject.transform.childCount == 0)
        {
            MaterialCost[] materialCost = towerUpgrades.FindTowerByType(towerUpgrade).GetMaterialCosts();
            foreach (MaterialCost material in materialCost)
            {
                BuildMaterial buildMaterial = material.GetBuildMaterial();
                MaterialUse materialUse = buildMaterialCollection.GetMaterialUse(buildMaterial);

                GameObject materialCountInstance = material.GetCount() <= materialUse.GetCount() ?
                    Instantiate(materialCountPrefab, gameObject.transform) :
                    Instantiate(notEnoughMaterialCountPrefab, gameObject.transform);

                MaterialCountUI materialCountUI = materialCountInstance.GetComponent<MaterialCountUI>();
                materialCountUI.SetMaterial(materialUse.GetBuildMaterialSO().GetSprite(), material.GetCount());
            }
        }
    }
    public void RemoveCostMaterialsUI()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            GameObject childrenGameObject = children[i].gameObject;
            if (childrenGameObject != gameObject) 
            {
                Destroy(childrenGameObject);
            }
        }

    }
}