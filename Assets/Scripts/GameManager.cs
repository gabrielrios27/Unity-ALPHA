using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PlayerCharacterController.onDeath+= GameOver;
    }
    void Update(){
        OnEscClick();
    }
    private void GameOver()
    {
        scoreInstanciate = 0;
        Debug.Log("Evento onDeath - llamado por : PlayerCharacterController - recibido por GameManager");
    }
    private void OnDestroy(){
       PlayerCharacterController.onDeath-= GameOver;
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
    private void OnEscClick(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string levelName = SceneManager.GetActiveScene().name;
            if(levelName=="Level3BaseMission" || levelName=="Level2Destroyed_city"){
               SceneManager.LoadScene("Level1Present"); 
            } 
            if(levelName=="Level1Present"){
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
            
        }
    }
    
}
