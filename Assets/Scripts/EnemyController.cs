using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   

    public float speedEnemy = 10;
    public float lifeEnemy = 7f;
    public Vector3 directionEnemy = Vector3.back;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        MoveToward();
        // destroyEnemy(directionEnemy);
    }
    private void MoveEnemy(Vector3 direction){
        transform.Translate(speedEnemy * Time.deltaTime * direction);
    }
    private void destroyEnemy(Vector3 direction){
        lifeEnemy -= Time.deltaTime;
        if(lifeEnemy > 0){
            MoveEnemy(direction);
        }else{
            Destroy(gameObject);
        }
    }
    private void MoveToward(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += speedEnemy * Time.deltaTime * direction;
    }
    private void LookAtPlayer(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = newRotation; 
    }
}
