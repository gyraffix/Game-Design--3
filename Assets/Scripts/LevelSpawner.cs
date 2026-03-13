using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDistance;
    
    [SerializeField] private GameObject[] levelList;
    [SerializeField] private List<GameObject> currentActiveLevels;
    private int playedLevels;

    [HideInInspector] public float gameSpeed = 1;
    public static LevelSpawner instance;

    private bool allLevelsPlayed;

    private void Start()
    {
        foreach (var level in levelList)
            currentActiveLevels.Add(level);                        
    }

    private void Awake()
    {
        instance = this;
        gameSpeed = 1;        
    }


    public IEnumerator SpawnNewLevel(Transform spawnPoint)
    {
        yield return new WaitForSeconds(1);

        if (currentActiveLevels.Count == 0)
        {
            foreach (var level in levelList)
                currentActiveLevels.Add(level);  
            allLevelsPlayed = true;
            playedLevels = 0;
            gameSpeed += 0.1f;
        }

        //Debug.Log("Spawn");
        Vector3 spawnPos = spawnPoint.position + (spawnPoint.right * -spawnDistance);

        int levelToSpawn = 0;
        GameObject spawnedLevel = null;

        if (!allLevelsPlayed)
        {
            levelToSpawn = 0;
            spawnedLevel = Instantiate(currentActiveLevels[0], spawnPos, spawnPoint.rotation);
        }
        else
        {
            levelToSpawn = Random.Range(0, currentActiveLevels.Count);
            spawnedLevel = Instantiate(currentActiveLevels[levelToSpawn], spawnPos, spawnPoint.rotation);
        }
        currentActiveLevels.Remove(currentActiveLevels[levelToSpawn]);

        playedLevels++;

        if (spawnedLevel.transform.Find("StaticObjects") != null)
        {
            Transform staticObjectsTransform = spawnedLevel.transform.Find("StaticObjects").transform;
            staticObjectsTransform.parent = GameObject.Find("NewLevelPlatform").transform;
            //staticObjectsTransform.position = staticObjectsTransform.position + staticObjectsTransform.right * spawnDistance;
        }
    }
}
