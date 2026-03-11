using NUnit.Framework;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThirdPersonController>())
        {
            gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(LevelSpawner.instance.SpawnNewLevel(transform));
            Debug.Log("NoCollider");
        }
    }    
}
