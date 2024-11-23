using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerMouseDetector : MonoBehaviour
{
    TowerUpgradeControl control;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        FindControl();

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, transform.forward);
            foreach (RaycastHit2D hit in hits)
            {
                GameObject hitGO = hit.collider.gameObject;
                if (hitGO == gameObject)
                {
                    control.ToggleUI(true);
                    return;
                }
            }
            control.ToggleUI(false);
        }
    }

    private void FindControl()
    {
        Debug.Log("detect click");
        control = gameObject.transform.parent.GetComponent<TowerUpgradeControl>();

        if (control == null)
        {
            control = gameObject.GetComponent<TowerUpgradeControl>();
        }
    }
}
