using NUnit.Framework;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelTrigger : MonoBehaviour
{
    private AudioSource ding;
    [HideInInspector] public MovingLevel previousLevel;


    private void Start()
    {
        ding = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThirdPersonController>())
        {
            gameObject.GetComponent<Collider>().enabled = false;
            if (previousLevel != null)
            previousLevel.DestroyLevel();
            StartCoroutine(LevelSpawner.instance.SpawnNewLevel(transform));
            Debug.Log("NoCollider");
            ding.Play();
            GameLoop.instance.AddScore();
        }
    }    
}
