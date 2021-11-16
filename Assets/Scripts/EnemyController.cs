using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   
    [SerializeField] private float speedEnemy = 3;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float lifeEnemy = 10f;
    [SerializeField] private float armorEnemy = 2f;
     private GameObject player;
    [SerializeField] private float speedToLook = 3f;
    enum typeOfEnemys {spectator=1 , runner, killer};
    [SerializeField] private typeOfEnemys typeOfEnemy;
    private Rigidbody rbEnemy;
    [SerializeField] private Animator animEnemy;
    private bool isAttack = false;
    private bool isRun = false;
    private bool isLeader =false;

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
      rbEnemy = GetComponent<Rigidbody>();
      animEnemy = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animEnemy.SetBool("isRun", isRun);
        animEnemy.SetBool("isAttack", isAttack);
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
      

        if(direction.magnitude > attackRange)
        {
            isAttack = false;
            isRun = true;
            rbEnemy.AddForce(direction.normalized * speedEnemy, ForceMode.Impulse);
        }
        else
        {
            isAttack = true;
            isRun = false;
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
                isLeader =true;
                break;
            case typeOfEnemys.runner:
                HuntLookDied();
                isLeader =false;
                break;
            case typeOfEnemys.killer:
                HuntLookDied();
                isLeader =false;
                break;
            default:
                LookAtPlayer();
                isLeader =true;
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
                    if(isLeader){
                        Debug.Log("ganaste!!");
                        Destroy(gameObject);
                    }else{
                        Destroy(gameObject);
                    }
                 
                }
  
            }
            
        }
  
}
