using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveSO", fileName ="New Wawe SO")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject[] enemies = new GameObject[0];
    public int GetEnemiesLength()
    {
        return enemies.Length;
    }
    public GameObject GetEnemyAt(int index)
    {
        return enemies[0];
    }
}
