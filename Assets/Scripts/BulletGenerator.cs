using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject BulletPrefabs;
     public float spawnInterval = 2f;
     public float startDelay = 1;
    

    void Start()
    {
        InvokeRepeating("SpawnBullet", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnBullet()
    {
        
        Instantiate(BulletPrefabs, transform.position, BulletPrefabs.transform.rotation);
    }
}
