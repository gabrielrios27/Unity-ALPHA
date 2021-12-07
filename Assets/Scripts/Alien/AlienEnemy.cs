using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemy : MonoBehaviour
{
    private float lifeEnemy;
    private float armorEnemy;
    
    
    [SerializeField] protected AlienData myData;
    protected Rigidbody rbEnemy;
    [SerializeField] private Animator animEnemy;
    private GameObject player;

    protected bool isAttack = false;
    protected bool isRun = false;
    protected bool isLeader =false;

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
      rbEnemy = GetComponent<Rigidbody>();
      animEnemy = gameObject.transform.GetChild(0).GetComponent<Animator>();
      lifeEnemy = myData.HP;
      armorEnemy = myData.Armor;

    }

    // Update is called once per frame
    void Update()
    {
        animEnemy.SetBool("isRun", isRun);
        animEnemy.SetBool("isAttack", isAttack);
    }

    private void FixedUpdate()
    {
        HuntLookDied();
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
    
    public virtual void MoveToward(){
        Vector3 direction = GetPlayerDirection();
        if(direction.magnitude > myData.AttackRange)
        {
            isAttack = false;
            isRun = true;
            rbEnemy.AddForce(direction.normalized * myData.Speed, ForceMode.Impulse);
        }
        else
        {
            isAttack = true;
            isRun = false;
            rbEnemy.velocity= Vector3.zero;
        }
        
       
    }
    private void LookAtPlayer(){
        Vector3 direction = GetPlayerDirection();
        Vector3 newDirection = new Vector3(direction.x,0,direction.z);
        Quaternion newRotation = Quaternion.LookRotation(newDirection);
        rbEnemy.rotation = Quaternion.Lerp(transform.rotation, newRotation, myData.SpeedToLook * Time.deltaTime);
         
    }
    protected Vector3 GetPlayerDirection()
    {
         if (player)
            {
                return player.transform.position - transform.position;
            }
        else{
            return new Vector3(0,0,0);
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
                        GameManager.instance.addWinScore();
                        Debug.Log(GameManager.GetScore());
                        Debug.Log("ganaste!!");
                        Destroy(gameObject);
                    }else{
                        GameManager.instance.addScore();
                        Debug.Log(GameManager.GetScore());
                        Destroy(gameObject);
                    }
                 
                }
  
            }
            
        }
   
  
}
