using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    // Start is called before the first frame update
     [SerializeField] private GameObject[] enemyPrefabs;
     [SerializeField] private float spawnInterval = 3f;
     [SerializeField] private float startDelay = 2;
    enum Difficulties {Easy=1 , Normal, Hard};
    [SerializeField] private Difficulties difficulty;
    private float howEasy = 3f;
    private float howHard = 2f;
    private int enemyIndex = 0;
    void Start()
    {
        SwitchDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        // int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], transform.position, enemyPrefabs[enemyIndex].transform.rotation);
    }
    void SwitchDifficulty(){
        switch (difficulty)
        {
            case Difficulties.Easy:
                InvokeRepeating("SpawnEnemy", startDelay + howEasy, spawnInterval + howEasy);
                enemyIndex = 0;
                break;
            case Difficulties.Normal:
                InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
                enemyIndex = 0;
                break;
            case Difficulties.Hard:
                InvokeRepeating("SpawnEnemy", startDelay - howHard, spawnInterval - howHard);
                enemyIndex = 1;
                break;
            default:
                Debug.Log("Error! la dificultad elegida no se encuentra");
                break;
        }
    }
}
