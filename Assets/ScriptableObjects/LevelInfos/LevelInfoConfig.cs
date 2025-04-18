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
        LevelWave levelWave = Array.Find(levelWaves, levelWave => levelWave.pathName == pathName);
        return levelWave?.wavesSO;       
    }
}

[Serializable]
public class LevelWave
{
    [SerializeField] public string pathName;
    [SerializeField] public WaveConfig[] wavesSO;
}

