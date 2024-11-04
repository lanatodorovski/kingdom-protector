using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathScript : MonoBehaviour
{
    [SerializeField] GameObject[] pathPoints = new GameObject[0];
    [SerializeField] WaveConfig[] waveSO = new WaveConfig[0];
    [SerializeField]
    GameObject[] spawnPoints = new GameObject[0];
    [SerializeField] float enemySpawnDelay = 1.0f;


    private WaveConfig currentWave;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = waveSO[0];
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        for (int i = 0; i < currentWave.GetEnemiesLength(); i++)
        {
            Instantiate(currentWave.GetEnemyAt(i), spawnPoints[Random.Range(0, spawnPoints.Length)].transform);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }

    public GameObject GetPathPointAt(int index)
    {
        return pathPoints[index];
    }
    public Vector2 GetRandomPositionFromPoint(int index, float radius = 1)
    {
        return pathPoints[index].transform.position + (Vector3) Random.insideUnitCircle * radius;
    }
    public int GetPathPointCount()
    {
        return pathPoints.Length;
    }
}
