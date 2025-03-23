using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool isPaused;

    
    [SerializeField] Canvas MenuUI;
    [SerializeField] Canvas SettingsUI;
    [SerializeField] Canvas DeathMenuUI;
    [SerializeField] Canvas LevelCompletionUI;
    [SerializeField] private bool disableOnStart;
    [SerializeField] private bool canToggleMenu;


    private Canvas ActiveUI;

    private void Awake()
    {
        isPaused = !disableOnStart;
    }
    private void Start()
    {
        if (disableOnStart && MenuUI != null) MenuUI.gameObject.SetActive(false);
        if(!disableOnStart) ActiveUI = MenuUI;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canToggleMenu)
        {
            ToggleMenuUI();
        }
    }
    public void ToggleMenuUI()
    {
        if(ActiveUI == null)
        {
            ActiveUI = MenuUI;
        }
        ToggleUI();
    }
    public void ToggleDeathUI()
    {
        if (ActiveUI == null)
        {
            ActiveUI = DeathMenuUI;
            canToggleMenu = false;
        }
        ToggleUI();
    }

    public void ToggleLevelCompletionUI()
    {
        if (ActiveUI == null)
        {
            ActiveUI = LevelCompletionUI;
            canToggleMenu = false;
        }
        ToggleUI();
    }
    public void ToggleUI() {
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
    public void ToggleSettingsUI()
    {
        ActiveUI.gameObject.SetActive(false);
        ActiveUI = ActiveUI == SettingsUI ? MenuUI : SettingsUI;
        ActiveUI.gameObject.SetActive(true);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("CastleScene");
        Time.timeScale = 1f;
    }
    public void RestartLevel()
    {

    }
    public void LoadGame()
    {

    }
    public void LoadExpeditionScene()
    {
        Time.timeScale = 1f;
        LocalSaveSystem localSaveSystem = FindAnyObjectByType<LocalSaveSystem>();
        localSaveSystem.SetHasGathered(false);
        localSaveSystem.SetMaterialCount(FindAnyObjectByType<BuildMaterialCollection>().GetAllAsMaterialCount());
        localSaveSystem.SetFieldTowerType();
        localSaveSystem.SaveGame();

        SceneManager.LoadScene("ExpeditionScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
