using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{    
    [SerializeField] private LevelConstruction[] levelConstructions;
    [SerializeField] private LevelDesign[] levelDesigns;
    [SerializeField] private GameObject pathsContainer;
    [SerializeField] private int forceLevel = 0; 
    private LevelConstruction currentLevel;
    private LevelDesign currentDesign;


    public static int level = 1;
    public static bool levelCompleted;

    private LocalSaveSystem saveSystem;
    private void Awake()
    {
        saveSystem = FindAnyObjectByType<LocalSaveSystem>();
    }
    private void Start()
    {
        bool finished = false;
        if (forceLevel == 0)
        {
            finished = !GetLocalSaveLevel();
        }
        else
        {
            level = forceLevel;
            if (forceLevel > levelConstructions.Length) finished = true;
        }
        

        int levelIndex = level - 1;
        if (finished) levelIndex--;
        currentLevel = levelConstructions[levelIndex];
        currentDesign = levelDesigns[currentLevel.GetLevelDesignId()];       
        HandleTowers();

        if (finished) return;
        SetWavesToPaths();        
        SetMapDesign();
    }
    private void SetWavesToPaths()
    {
        LevelInfoConfig currentLevelWaves = currentLevel.GetLevelWaves();
        EnemyPathScript[] enemyPathScripts = pathsContainer.GetComponentsInChildren<EnemyPathScript>();
        foreach(EnemyPathScript enemyPathScript in enemyPathScripts)
        {
            WaveConfig[] waves = currentLevelWaves.GetWavesByPathName(enemyPathScript.gameObject.name);
            //Debug.Log(enemyPathScript.gameObject.name + " " + waves.Length);
            if (waves != null && waves.Length != 0)
            {
                    enemyPathScript.SetWavesSo(waves);
            }
            else
            {
                enemyPathScript.isAllSpawned = true;
            }           
        }
    }

    private void HandleTowers()
    {
        TowerUpgradeControl.SetTowerScripts();
        
        GameObject[] deactivatedFields = currentDesign.GetDeactivatedFields();
        if(deactivatedFields != null && deactivatedFields.Length != 0)
        {
            foreach(GameObject field in deactivatedFields)
            {
                field.SetActive(false);
            }
        }
    }
    private void SetMapDesign()
    {
        currentDesign.GetLevelMap().SetActive(true);
    }

    private bool GetLocalSaveLevel() //returns true if the level was managed to be found, false if all the levels are completed
    {
        level = saveSystem.LoadSave().level;
        if (level > levelConstructions.Length)
        {
            Debug.Log(level + " " + levelConstructions.Length);            
            FindAnyObjectByType<MenuManager>().ToggleCanvas("GameCompletionUI", false);
            return false;
        }
        return true;
    }
    public IEnumerator SuccessfullyEndLevel(float levelCompletionDelay)
    {    

        LocalSaveSystem localSaveSystem = FindAnyObjectByType<LocalSaveSystem>();

        yield return new WaitForSecondsRealtime(levelCompletionDelay);
        localSaveSystem.NextLevel();
        level++;
        if (level <= levelConstructions.Length)
        {
            FindAnyObjectByType<MenuManager>().ToggleCanvas("LevelCompletionUI", false);
            localSaveSystem.SetHasGathered(false);            
        }
        else
        {
            FindAnyObjectByType<MenuManager>().ToggleCanvas("GameCompletionUI", false);
        }   
        localSaveSystem.SetMaterialCount(FindAnyObjectByType<BuildMaterialCollection>().GetAllAsMaterialCount());
        localSaveSystem.SetFieldTowerType();
        localSaveSystem.SaveGame();



    }
}
[Serializable]
public class LevelConstruction
{

    [SerializeField] private LevelInfoConfig levelWaves;
    [SerializeField] private int levelDesignId;
    public LevelInfoConfig GetLevelWaves() {
        return levelWaves;
    }
    public int GetLevelDesignId()
    {
        return levelDesignId;
    }

}

[Serializable]
public class LevelDesign
{
    [SerializeField] private GameObject levelMap;
    [SerializeField] private GameObject[] deactivatedFields;
    public GameObject GetLevelMap()
    {
        return levelMap;
    }
    public GameObject[] GetDeactivatedFields()
    {
        return deactivatedFields;
    }
}
