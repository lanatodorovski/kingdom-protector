using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    [SerializeField] Canvas PauseMenuUI;
    [SerializeField] private bool disableOnStart;
    private void Start()
    {
        if (disableOnStart && PauseMenuUI != null) PauseMenuUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    public void TogglePauseMenu()
    {
        PauseMenuUI.gameObject.SetActive(!PauseMenuUI.gameObject.activeSelf);
    }
}
