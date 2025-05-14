using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathScript : MonoBehaviour
{
    [SerializeField] List<GameObject> pathPoints = new List<GameObject>();
    [SerializeField] WaveConfig[] waveSO = new WaveConfig[0];
    [SerializeField]
    GameObject[] spawnPoints = new GameObject[0];
    [SerializeField] float enemySpawnDelay = 1.0f;
    [SerializeField] float waveStartDelay = 2.0f;
    [SerializeField] float radius;

    [Header("Path Connectors")]
    [SerializeField] private GameObject previousPoint = null;
    [SerializeField] private GameObject nextPoint = null;    

    [Header("Main Path - Level Completion")]
    private static float levelCompletionDelay = 1.0f;


    private WaveConfig currentWave;    
    [HideInInspector] public bool isAllSpawned = false;
    EnemyPathScript[] enemyPathScripts;    
    private void Awake()
    {
        enemyPathScripts = transform.parent.GetComponentsInChildren<EnemyPathScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.levelCompleted = false;        
        AddAdditionalPoints();
       
    }

    IEnumerator StartWave()
    {
        for (int i = 0; i < currentWave.GetEnemiesLength(); i++)
        {
            GameObject enemy= Instantiate(currentWave.GetEnemyAt(i), spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform);           
            enemy.GetComponent<EnemyMovement>().SetPath(this);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
        Debug.Log("ALL ENEMIES SPAWNED");
        isAllSpawned = true;
    }
    IEnumerator StartLevel()
    {
        for(int i = 0; i< waveSO.Length; i++)
        {
            Debug.Log("New wave started");
            currentWave = waveSO[i];

            isAllSpawned = false;
            StartCoroutine(StartWave());

            yield return new WaitUntil(() => isWaveCompleted());

            if (i == waveSO.Length - 1) break;
            yield return new WaitForSeconds(waveStartDelay);

        }
        Debug.Log("Level finished");
        if (!LevelManager.levelCompleted)
        {
            LevelManager.levelCompleted = true;
            StartCoroutine(FindAnyObjectByType<LevelManager>().SuccessfullyEndLevel(levelCompletionDelay));
        }
        
    }

    private bool isWaveCompleted()
    {
        if (enemyPathScripts != null)
        {
            bool uncompletePathExists = Array.Find(enemyPathScripts,
                enemyPathScript => enemyPathScript.isAllSpawned == false
                || enemyPathScript.gameObject.GetComponentsInChildren<EnemyMovement>().Length > 0);
            //Debug.Log(uncompletePathExists + " " + (FindAnyObjectByType<PopulationHandler>().GetPopulationCount() > 0));
            if (!uncompletePathExists && FindAnyObjectByType<PopulationHandler>().GetPopulationCount() > 0)
            {
                return true;                
            }
        }
        return false;
    }

    public GameObject GetPathPointAt(int index)
    {
        return pathPoints[index];
    }
    public Vector2 GetRandomPositionFromPoint(int index)
    {
        return pathPoints[index].transform.position + (Vector3) UnityEngine.Random.insideUnitCircle * radius;
    }
    public int GetPathPointCount()
    {
        return pathPoints.Count;
    }
    public List<GameObject> GetPointRangeFromPoint(GameObject selectedPoint, bool reverseOrder = false)
    {
        int index = pathPoints.IndexOf(selectedPoint);
        if(index > -1) { 
               List<GameObject> result = new List<GameObject>();
                result.AddRange(
                   !reverseOrder?
                   pathPoints.GetRange(index ,  pathPoints.Count - index ): //From point to the last 
                   pathPoints.GetRange(0, index + 1) // From start to the point
                );
               return result;
         
        }
        return null;
    }
    private void AddAdditionalPoints()
    {
        if (previousPoint != null)
        {
            EnemyPathScript switchFromPath = previousPoint.transform.parent.GetComponent<EnemyPathScript>();
            List<GameObject> addedPoints = switchFromPath.GetPointRangeFromPoint(previousPoint, true);
            pathPoints.InsertRange(0, addedPoints);
            //pathPoints.RemoveAt(0);
        }
        if (nextPoint != null)
        {
            EnemyPathScript switchToPath = nextPoint.transform.parent.GetComponent<EnemyPathScript>();
            List<GameObject> addedPoints = switchToPath.GetPointRangeFromPoint(nextPoint);
            pathPoints.AddRange(addedPoints);
            //Debug.Log(GetPathPointCount());
        }
    }

    public void SetWavesSo(WaveConfig[] waves) { 
        this.waveSO = waves;        
        StartCoroutine(StartLevel());
    }
}
