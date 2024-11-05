using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathScript : MonoBehaviour
{
    [SerializeField] List<GameObject> pathPoints = new List<GameObject>();
    [SerializeField] WaveConfig[] waveSO = new WaveConfig[0];
    [SerializeField]
    GameObject[] spawnPoints = new GameObject[0];
    [SerializeField] float enemySpawnDelay = 1.0f;

    [Header("Next Path")]
    [SerializeField] private EnemyPathScript switchToPath = null;
    [SerializeField] private GameObject nextPoint = null;

    private WaveConfig currentWave;
    private List<GameObject> AdditionalPathPoints;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = waveSO[0];
        AddAdditionalPoints();
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        for (int i = 0; i < currentWave.GetEnemiesLength(); i++)
        {
            GameObject enemy= Instantiate(currentWave.GetEnemyAt(i), spawnPoints[Random.Range(0, spawnPoints.Length)].transform);
            enemy.GetComponent<EnemyMovement>().SetPath(this);
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
        return pathPoints.Count;
    }
    public List<GameObject> GetPointRangeFromPoint(GameObject selectedPoint)
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            GameObject point = pathPoints[i];
            if(point == selectedPoint)
            {
               List<GameObject> result = new List<GameObject>();
               result.AddRange(pathPoints.GetRange(i, pathPoints.Count - i));
               return result;
            }
        }
        return null;
    }
    private void AddAdditionalPoints()
    {
        if(switchToPath != null && nextPoint != null)
        {
            List<GameObject> addedPoints = switchToPath.GetPointRangeFromPoint(nextPoint);
            pathPoints.AddRange(addedPoints);
            Debug.Log(GetPathPointCount());
        }
    }
}
