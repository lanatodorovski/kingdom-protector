using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelDetails[] levelDetails;
    [SerializeField] private LevelDesign[] levelDesigns;
    [SerializeField] private GameObject pathsContainer;

    private LevelDetails currentLevel;
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
        GetLocalSaveLevel();

        SetWavesToPaths();
        DeactivateUnneededFields();
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

    private void DeactivateUnneededFields()
    {
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

    private void GetLocalSaveLevel()
    {
        level = saveSystem.LoadSave().level;
        currentLevel = levelDetails[level - 1];
        currentDesign = levelDesigns[currentLevel.GetLevelDesignId()];
    }
}
[Serializable]
public class LevelDetails
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
