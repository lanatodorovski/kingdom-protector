using System;
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
    [SerializeField] float radius;

    [Header("Next Path")]
    [SerializeField] private GameObject nextPoint = null;
    private EnemyPathScript switchToPath = null;

    [Header("Main Path - Level Completion")]
    private static float levelCompletionDelay = 1.0f;
    public static bool levelCompleted;

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
        levelCompleted = false;
        currentWave = waveSO[0];
        AddAdditionalPoints();
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (enemyPathScripts != null && !levelCompleted)
        {
            bool uncompletePathExists = Array.Find(enemyPathScripts, 
                enemyPathScript => enemyPathScript.isAllSpawned == false
                || enemyPathScript.gameObject.GetComponentsInChildren<EnemyMovement>().Length > 0);
            Debug.Log(uncompletePathExists + " " + (FindAnyObjectByType<PopulationHandler>().GetPopulationCount() > 0));
            if (!uncompletePathExists && FindAnyObjectByType<PopulationHandler>().GetPopulationCount() > 0)
            {
                StartCoroutine(SuccessfullyEndLevel());
                levelCompleted = true;
            }
        }
                    
        //if (isAllSpawned && transform.parent.GetComponentsInChildren<EnemyMovement>().Length == 0 && FindAnyObjectByType<PopulationHandler>().GetPopulationCount() > 0)
        //{            
        //    StartCoroutine(SuccessfullyEndLevel());
        //    Debug.Log("WAVE IS DONE!");
            
        //}
       

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
    IEnumerator SuccessfullyEndLevel()
    {
        yield return new WaitForSecondsRealtime(levelCompletionDelay);
        FindAnyObjectByType<MenuManager>().ToggleLevelCompletionUI();

        LocalSaveSystem localSaveSystem = FindAnyObjectByType<LocalSaveSystem>();
        localSaveSystem.SetHasGathered(false);
        localSaveSystem.SetMaterialCount(FindAnyObjectByType<BuildMaterialCollection>().GetAllAsMaterialCount());
        localSaveSystem.SetFieldTowerType();
        localSaveSystem.SaveGame();
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
        if(nextPoint != null)
        {
            switchToPath = nextPoint.transform.parent.GetComponent<EnemyPathScript>();
            List<GameObject> addedPoints = switchToPath.GetPointRangeFromPoint(nextPoint);
            pathPoints.AddRange(addedPoints);
            //Debug.Log(GetPathPointCount());
        }
    }
}
