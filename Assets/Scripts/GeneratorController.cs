using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject[] enemyPrefabs;
     public float spawnInterval = 2f;
     public float startDelay = 1;
    

    void Start()
    {
        //Instantiate(enemyPrefab, transform);
        //Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        //Debug.Log(Quaternion.identity);
        //Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        //int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        //Instantiate(enemyPrefabs[enemyIndex], transform.position, enemyPrefabs[enemyIndex].transform.rotation);
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], transform.position, enemyPrefabs[enemyIndex].transform.rotation);
    }

}
