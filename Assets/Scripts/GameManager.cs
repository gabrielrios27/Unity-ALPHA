using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
  
    public static int score;

    private int scoreInstanciate; 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            score = 0;
            scoreInstanciate = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayerController.onDeath+= GameOver;
    }
    private void GameOver()
    {
        scoreInstanciate = 0;
        Debug.Log("Evento onDeath - llamado por : PlayerController - recibido por GameManager");
    }
    public void addScore()
    {
        instance.scoreInstanciate += 1;  
    }
    public void addWinScore()
    {
        instance.scoreInstanciate += 10;  
    }
    public static int GetScore()
    {
        return instance.scoreInstanciate;
    }
    
}
