using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool isPaused;

    
    [SerializeField] Canvas CurrentMenuUI;
    [SerializeField] private bool disableOnStart;
    [SerializeField] private bool canToggleMenu;

    [SerializeField] Canvas SettingsUI;

    private Canvas ActiveUI;

    private void Awake()
    {
        isPaused = !disableOnStart;
    }
    private void Start()
    {
        if (disableOnStart && CurrentMenuUI != null) CurrentMenuUI.gameObject.SetActive(false);
        if(!disableOnStart) ActiveUI = CurrentMenuUI;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canToggleMenu)
        {
            ToggleActiveUI();
        }
    }
    public void ToggleActiveUI()
    {
        if(ActiveUI == null)
        {
            ActiveUI = CurrentMenuUI;
        }
        isPaused = !isPaused;
        ActiveUI.gameObject.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        if (!isPaused) ActiveUI = null;
    }
    public void SaveAndQuit()
    {
        //MAKE A FUNCTONALITY TO SAVE LOCALY HERE
        Application.Quit();
    }
    public void ToggleSettings()
    {
        ActiveUI.gameObject.SetActive(false);
        ActiveUI = ActiveUI == SettingsUI ? CurrentMenuUI : SettingsUI;
        ActiveUI.gameObject.SetActive(true);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("CastleScene");
        Time.timeScale = 1f;
    }
    public void LoadGame()
    {

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
