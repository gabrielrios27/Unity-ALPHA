using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   
    [SerializeField] private float speedEnemy = 3;
    [SerializeField] private float lifeEnemy = 10f;
    [SerializeField] private float armorEnemy = 2f;
     private GameObject player;
    [SerializeField] private float speedToLook = 3f;
    enum typeOfEnemys {spectator=1 , runner, killer};
    [SerializeField] private typeOfEnemys typeOfEnemy;
    private Rigidbody rbEnemy;

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
      rbEnemy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void FixedUpdate()
    {
        SwitchEnemy();
    }
    
    private void HuntLookDied(){
        lifeEnemy -= Time.deltaTime;
        
        if(lifeEnemy > 0){
            LookAtPlayer();
            MoveToward();
        }else{
            Destroy(gameObject);
        }
    }
    
    private void MoveToward(){
        Vector3 direction = GetPlayerDirection();
        if(direction.magnitude > 2 && typeOfEnemy==typeOfEnemys.runner){
            rbEnemy.AddForce(direction.normalized * speedEnemy);
        }else if(typeOfEnemy==typeOfEnemys.killer){
            rbEnemy.AddForce(direction.normalized * speedEnemy);
            // transform.position += speedEnemy * Time.deltaTime * direction.normalized;
        }
    }
    private void LookAtPlayer(){
        Vector3 direction = GetPlayerDirection();
        Vector3 newDirection = new Vector3(direction.x,0,direction.z);
        Quaternion newRotation = Quaternion.LookRotation(newDirection);
        rbEnemy.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime);
         
    }
    private Vector3 GetPlayerDirection()
    {
        return player.transform.position - transform.position;
    }
    void SwitchEnemy(){
        switch (typeOfEnemy)
        {
            case typeOfEnemys.spectator:
                LookAtPlayer();
                break;
            case typeOfEnemys.runner:
                HuntLookDied();
                break;
            case typeOfEnemys.killer:
                HuntLookDied();
                break;
            default:
                LookAtPlayer();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                armorEnemy--;
                Destroy(other.gameObject);
                if(armorEnemy==0){
                    Destroy(gameObject);
                }
  
            }
            
        }
  
}
