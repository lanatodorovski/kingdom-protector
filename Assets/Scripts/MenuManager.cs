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
    public static bool isPaused = false;
    
    [SerializeField] private bool canToggleMenu;

    [SerializeField] private Canvas[] controllUI;
    private Canvas ActiveUI = null;
    private Stack<Canvas> previousUIs;

    private void Awake()
    {       
        previousUIs = new Stack<Canvas>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            ActiveUI = GetCanvas("MainMenuUI");
            isPaused = true;

            Canvas GameSlotUI = GetCanvas("SaveSlotsUI");
            if (GameSlotUI != null) SetupGameSlotUI(GameSlotUI);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canToggleMenu)
        {
            ToggleCanvas("PauseMenuUI");
        }
    }

    //CANVAS FUNCTIONS
    public void ToggleUI()
    {
        isPaused = !isPaused;
        ActiveUI.gameObject.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        if (!isPaused) ActiveUI = null;
        if (SceneManager.GetActiveScene().name == "CastleScene") ToggleTowerInteratction();
    }


    public Canvas GetCanvas(string canvasName)
    {
        return Array.Find(controllUI, canvas => canvas.gameObject.name == canvasName);
    }
    public void ToggleCanvas(string canvasName)
    {
        Canvas canvas = GetCanvas(canvasName);
        ToggleCanvas(canvas);                
    }
    public void ToggleCanvas(string canvasName, bool canToggleMenu)
    {
        ToggleCanvas(canvasName);
        this.canToggleMenu = canToggleMenu;
    }

    public void ToggleCanvas(Canvas toggleCanvas)
    {
        if (ActiveUI == null)
        {
            ActiveUI = toggleCanvas;            
        }
        ToggleUI();
    }
    public void SetCanToggleMenu(bool canToggleMenu)
    {
        this.canToggleMenu = canToggleMenu;
    }

    public void SetPreviousUI(Canvas canvas)
    {        
        previousUIs.Push(canvas);
        ToggleUI();
    }
    public void TogglePreviousCanvas()
    {
        if(previousUIs.Count > 0)
        {
            ToggleCanvas(previousUIs.Pop());
        }
    }

    //SCENE AND APLICATION FUNCTIONS    
    public void SaveAndQuit()
    {        
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("CastleScene");
        Time.timeScale = 1f;
    }

    [ContextMenu("Reset Level")]
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    public void OpenProjectUrl()
    {
        Application.OpenURL("https://github.com/lanatodorovski/kingdom-protector");
    }

    //SETUPS
    private void SetupGameSlotUI(Canvas GameSlotUI)
    {        
        Button[] loadButtons = GameSlotUI.GetComponentsInChildren<Button>();


        LocalSaveSystem saveSystem = FindAnyObjectByType<LocalSaveSystem>();
        for (int i = 0; i < 3; i++)
        {
            TextMeshProUGUI[] textFields = loadButtons[i].GetComponentsInChildren<TextMeshProUGUI>();
            SaveSlotData saveSlot = saveSystem.LoadSave(i);
            textFields[1].text = "Level: ";
            
            if(saveSlot.level == 1 && !saveSlot.hasGathered || saveSlot == null)
            {
                textFields[1].text += "/";
            }
            else
            {
                textFields[1].text += saveSlot.level.ToString();
                textFields[1].text += saveSlot.hasGathered ? " - defend" : " - expedition";
            }
                                
            textFields[2].text = "Last Saved: " + (saveSlot.GetLastSaved() == DateTime.MinValue || saveSlot == null ? "/" : saveSlot.lastSaved.ToString());
        }
    }

    private void ToggleTowerInteratction()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");     
        Array.ForEach(towers, tower => {
            tower.GetComponent<TowerUpgradeControl>().ToggleUI(false);
            
            TowerMouseDetector? towerMouseDetector = tower.GetComponent<TowerMouseDetector>();
            if(towerMouseDetector != null) towerMouseDetector.enabled = !towerMouseDetector.enabled;            
        });
    }
}