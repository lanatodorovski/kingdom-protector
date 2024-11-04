using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveSO", fileName ="New Wawe SO")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private List<GameObject> enemies;
    public int GetEnemiesLength()
    {
        return enemies.Count;
    }
    public GameObject GetEnemyAt(int index)
    {
        return enemies[0];
    }
}
