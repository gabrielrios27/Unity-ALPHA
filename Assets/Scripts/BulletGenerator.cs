using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator: MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefabs;
     [SerializeField] private float spawnInterval = 2f;
     [SerializeField] private float startDelay = 1;
    

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
