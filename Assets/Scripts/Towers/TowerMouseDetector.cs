using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    }
    private void Start()
    {
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
                    if(control != null) control.ToggleUI(true);
                    return;
                }
            }
            if (control != null) control.ToggleUI(false);
        }
    }
    private void FindControl()
    {
        //Debug.Log("detect click" + gameObject.name);
        if (gameObject.transform.parent.tag == "Tower")
        {
            control = gameObject.transform.parent.GetComponent<TowerUpgradeControl>() ;
        }else
        {
            control = gameObject.GetComponent<TowerUpgradeControl>();
        }
    }
}
