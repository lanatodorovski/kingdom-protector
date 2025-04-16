using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelInfoConfig[] levelConfigs;
    [SerializeField] private GameObject[] levelMap;
    [SerializeField] private GameObject pathsContainer;
    private LevelInfoConfig currentLevel;


    public static int level = 1;
    public static bool levelCompleted;


    private void Awake()
    {
        currentLevel = levelConfigs[level - 1];
    }
    private void Start()
    {
        SetWavesToPaths();     
    }
    private void SetWavesToPaths()
    {
        EnemyPathScript[] enemyPathScripts = pathsContainer.GetComponentsInChildren<EnemyPathScript>();
        foreach(EnemyPathScript enemyPathScript in enemyPathScripts)
        {
            WaveConfig[] waves = currentLevel.GetWavesByPathName(enemyPathScript.gameObject.name);
            enemyPathScript.SetWavesSo(waves);
        }
    }
}
