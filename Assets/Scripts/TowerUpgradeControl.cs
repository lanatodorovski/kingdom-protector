using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TowerUpgradeControl : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] TowerType towerType = TowerType.Plain;

    TowerUpgradesDictionary towerUpgradesDictionary;
    private void Awake()
    {
        towerUpgradesDictionary = FindAnyObjectByType<TowerUpgrades>().towerUpgradesDictionary;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("aa");
        SetUpdade();
    }

    private void SetUpdade()
    {
        GameObject upgradedTower = Instantiate(towerUpgradesDictionary.findTowerByType(TowerType.Basic));
        upgradedTower.transform.parent = gameObject.transform.parent;
        upgradedTower.transform.localPosition = gameObject.transform.localPosition;
        upgradedTower.transform.localRotation = gameObject.transform.localRotation;
        upgradedTower.transform.localScale = gameObject.transform.localScale;
        upgradedTower.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex());
        Destroy(gameObject);
    }
}
