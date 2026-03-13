using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDistance;

    [SerializeField] private GameObject[] easyLevels;
    [SerializeField] private GameObject[] mediumLevels;
    [SerializeField] private GameObject[] hardLevels;
    [SerializeField] private List<GameObject> playedLevels;

    [HideInInspector] public float gameSpeed = 1;
    public static LevelSpawner instance;

    private void Awake()
    {
        instance = this;
        gameSpeed = 1;
    }


    public IEnumerator SpawnNewLevel(Transform spawnPoint)
    {
        yield return new WaitForSeconds(1);

        if (playedLevels.Count == 9)
        {
            playedLevels.Clear();
            gameSpeed += 0.1f;
        }

        //Debug.Log("Spawn");
        Vector3 spawnPos = spawnPoint.position + (spawnPoint.right * -spawnDistance);

        GameObject levelToSpawn = null;

        if (playedLevels.Count >= 0 && playedLevels.Count < easyLevels.Length)
        {
            levelToSpawn = Instantiate(easyLevels[playedLevels.Count], spawnPos, spawnPoint.rotation);
        }
        else if (playedLevels.Count >= easyLevels.Length && playedLevels.Count < easyLevels.Length + mediumLevels.Length)
        {
            levelToSpawn = Instantiate(mediumLevels[playedLevels.Count - easyLevels.Length], spawnPos, spawnPoint.rotation);
        }
        else
        {
            levelToSpawn = Instantiate(hardLevels[playedLevels.Count - (easyLevels.Length + mediumLevels.Length)], spawnPos, spawnPoint.rotation);
        }
        
        playedLevels.Add(levelToSpawn);

        if (levelToSpawn.transform.Find("StaticObjects") != null)
        {
            Transform staticObjectsTransform = levelToSpawn.transform.Find("StaticObjects").transform;
            staticObjectsTransform.parent = GameObject.Find("NewLevelPlatform").transform;
            //staticObjectsTransform.position = staticObjectsTransform.position + staticObjectsTransform.right * spawnDistance;
        }
    }
}
