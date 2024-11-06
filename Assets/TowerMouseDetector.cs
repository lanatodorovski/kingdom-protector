using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerMouseDetector : MonoBehaviour
{
    TowerUpgradeControl control;
    private void OnMouseDown()
    {
        
        control = gameObject.transform.parent.GetComponent<TowerUpgradeControl>();

        if (control == null)
        {
            control = gameObject.GetComponent<TowerUpgradeControl>();
        }

        control.ActivateUI();
        
    }
}
