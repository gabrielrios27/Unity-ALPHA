using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   

    public float speedEnemy = 3;
    public float lifeEnemy = 7f;
    public Vector3 directionEnemy = Vector3.back;
    private GameObject player;
    [SerializeField] private float speedToLook = 3f;
    

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        destroyEnemy();
        // destroyEnemy(directionEnemy);
    }
   
    private void destroyEnemy(){
        lifeEnemy -= Time.deltaTime;
        if(lifeEnemy > 0){
            MoveToward();
        }else{
            Destroy(gameObject);
        }
    }
    private void MoveToward(){
        Vector3 direction = (player.transform.position - transform.position);
        if(direction.magnitude > 2){
            transform.position += speedEnemy * Time.deltaTime * direction.normalized;
        }
    }
    private void LookAtPlayer(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime); 
    }
  
}
