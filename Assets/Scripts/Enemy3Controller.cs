using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedEnemy3 = 3;
    public float lifeEnemy3 = 30f;
    public Vector3 directionEnemy3 = Vector3.back;
    private GameObject player;
    [SerializeField] private float speedToLook = 3f;
    enum typeOfEnemys {spectator=1 , runner};
    [SerializeField] private typeOfEnemys typeOfEnemy;

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
        SwitchEnemy();
     
    }
   
    private void RunDestroyEnemy(){
        lifeEnemy3 -= Time.deltaTime;
        
        if(lifeEnemy3 > 0){
            LookAtPlayer();
            MoveToward();
        }else{
            Destroy(gameObject);
        }
    }
    private void SpectateDestroyEnemy(){
        lifeEnemy3 -= Time.deltaTime;
        if(lifeEnemy3 > 0){
            LookAtPlayer();
        }else{
            Destroy(gameObject);
        }
    }
    private void MoveToward(){
        Vector3 direction = (player.transform.position - transform.position);
        if(direction.magnitude > 2){
            transform.position += speedEnemy3 * Time.deltaTime * direction.normalized;
        }
    }
    private void LookAtPlayer(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime); 
    }
    void SwitchEnemy(){
        switch (typeOfEnemy)
        {
            case typeOfEnemys.spectator:
                SpectateDestroyEnemy();
                break;
            case typeOfEnemys.runner:
                RunDestroyEnemy();
                break;
            default:
                SpectateDestroyEnemy();
                break;
        }
    }
  
}
