using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] easyLevels;
    [SerializeField] private GameObject[] mediumLevels;
    [SerializeField] private GameObject[] hardLevels;
    [SerializeField] private List<GameObject> playedLevels;

    public static LevelSpawner instance;

    private void Awake()
    {
        instance = this;
    }


    public IEnumerator SpawnNewLevel(Transform spawnPoint)
    {
        yield return new WaitForSeconds(1);

        if (playedLevels.Count == 8)
        {
            playedLevels.Clear();
        }

        //Debug.Log("Spawn");
        Vector3 spawnPos = spawnPoint.position + (spawnPoint.right * -30);

        GameObject levelToSpawn = null;

        if (playedLevels.Count >= 0 && playedLevels.Count <= 2)
        {
            levelToSpawn = Instantiate(easyLevels[Random.Range(0, easyLevels.Length)], spawnPos, spawnPoint.rotation);
        }
        else if (playedLevels.Count >= 3 && playedLevels.Count <= 5)
        {
            levelToSpawn = Instantiate(mediumLevels[Random.Range(0, mediumLevels.Length)], spawnPos, spawnPoint.rotation);
        }
        else
        {
            levelToSpawn = Instantiate(hardLevels[Random.Range(0, hardLevels.Length)], spawnPos, spawnPoint.rotation);
        }
        
        playedLevels.Add(levelToSpawn);

        if (levelToSpawn.transform.Find("StaticObjects") != null)
        {
            Transform staticObjectsTransform = levelToSpawn.transform.Find("StaticObjects").transform;
            staticObjectsTransform.position = staticObjectsTransform.position + staticObjectsTransform.right * 30;
        }
    }
}
