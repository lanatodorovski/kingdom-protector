using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static bool isPaused;

    
    [SerializeField] Canvas MenuUI;
    [SerializeField] Canvas SettingsUI;
    [SerializeField] Canvas DeathMenuUI;
    [SerializeField] Canvas LevelCompletionUI;
    [SerializeField] Canvas GameSlotUI;
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
        if(GameSlotUI != null)SetupGameSlotUI();
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
    public void ToggleGameSlotUI()
    {
        ActiveUI.gameObject.SetActive(false);
        ActiveUI = ActiveUI == GameSlotUI ? MenuUI : GameSlotUI;
        ActiveUI.gameObject.SetActive(true);
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
        Time.timeScale = 1f;
        if (FindAnyObjectByType<LocalSaveSystem>().LoadSave().hasGathered)
        {
            SceneManager.LoadScene("CastleScene");
        }
        else
        {
            SceneManager.LoadScene("ExpeditionScene");
        }
    }
    public void LoadExpeditionScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ExpeditionScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetupGameSlotUI()
    {
        Button[] loadButtons = GameSlotUI.GetComponentsInChildren<Button>();        


        LocalSaveSystem saveSystem = FindAnyObjectByType<LocalSaveSystem>();
        for(int i = 0; i < 3; i++)
        {
            TextMeshProUGUI[] textFields = loadButtons[i].GetComponentsInChildren<TextMeshProUGUI>();            
            SaveSlotData saveSlot = saveSystem.LoadSave(i);            
            textFields[1].text = "Level: " + (saveSlot.level == 0 || saveSlot == null ? "/" : saveSlot.level.ToString());
            textFields[2].text = "Last Saved: " + (saveSlot.GetLastSaved() == DateTime.MinValue || saveSlot == null ? "/" : saveSlot.lastSaved.ToString());
        }
    }
}
