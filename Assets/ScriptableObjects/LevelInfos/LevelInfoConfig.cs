using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelInfoSO", fileName = "New LevelInfo SO")]
public class LevelInfoConfig : ScriptableObject
{
    public LevelWave[] levelWaves;

    public WaveConfig[] GetWavesByPathName(string pathName)
    {
        foreach (LevelWave levelWave in levelWaves)
        {
            levelWave.pathName = pathName;
            return levelWave.wavesSO;
        }

        return null;
    }
}

[Serializable]
public class LevelWave
{
    [SerializeField] public string pathName;
    [SerializeField] public WaveConfig[] wavesSO;
}

