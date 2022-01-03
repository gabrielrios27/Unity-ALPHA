using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemy : MonoBehaviour
{
    private float lifeEnemy;
    protected float armorEnemy;
    
    
    [SerializeField] protected AlienData myData;
    protected Rigidbody rbEnemy;
    [SerializeField] protected Animator animEnemy;
    protected GameObject player;

    protected bool isAttack = false;
    protected bool isRun = false;
    protected bool isLeader =false;

    // Variables para Patrullaje
    [SerializeField] protected Transform[] waypoints;
    [SerializeField] protected float speedPatrol = 5;
    [SerializeField] protected float rotationSpeed= 2;
    [SerializeField] protected float minimumDistance = 1;
    [SerializeField] protected GameObject character;
    [SerializeField] protected float rangeOfView = 15;
    protected float attackingRangeOfView;
    protected bool iSeeTheCharacter = false;
    protected int currentIndex = 0;
    protected bool goBack = false;
    protected bool isPatrolling = true;
    
    protected float aproachMagnitud=1000;
    protected bool flagAttack = false;
    [SerializeField] protected float timeAttack = 4f;
    protected float timeAttacking;
    
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
      rbEnemy = GetComponent<Rigidbody>();
      animEnemy = gameObject.transform.GetChild(0).GetComponent<Animator>();
      lifeEnemy = myData.HP;
      armorEnemy = myData.Armor;
      timeAttacking = timeAttack;
      attackingRangeOfView= rangeOfView+30;
    }

    // Update is called once per frame
    void Update()
    {
        animEnemy.SetBool("isRun", isRun);
        animEnemy.SetBool("isAttack", isAttack);
        animEnemy.SetBool("isPatrolling", isPatrolling);
    }

    public virtual void FixedUpdate()
    {
        if (player)
            {
                if (Vector3.Distance(transform.position, character.transform.position) <= rangeOfView)
                {
                    iSeeTheCharacter = true;
                    rangeOfView = attackingRangeOfView;
                }
                else
                {
                    iSeeTheCharacter = false;
                }
                Debug.Log("i see the character: "+ iSeeTheCharacter);
                if (iSeeTheCharacter)
                {
                    HuntLookDied();
                }
                else
                {
                    Patrol();
                }
            }
    }
    public virtual void Patrol()
    {
        isPatrolling = true;
        isRun = false;
        isAttack = false;
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;
        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * speedPatrol * Time.deltaTime;

        float distance = deltaVector.magnitude;

        if (distance < minimumDistance)
        {
            if (currentIndex >= waypoints.Length - 1)
            {
                goBack = true;
            }
            else if (currentIndex <= 0)
            {
                goBack = false;
            }

            if (!goBack)
            {
                currentIndex++;
            }
            else currentIndex--;
        }
    }
    

    private void OnDrawGizmos()
    {
        if (iSeeTheCharacter)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireSphere(transform.position, rangeOfView);
    }
   

    public virtual void HuntLookDied(){
        Vector3 direction = GetPlayerDirection();
        isPatrolling = false;
        LookAtPlayer();
        if(aproachMagnitud<direction.magnitude){
            rbEnemy.velocity= direction.normalized*myData.Speed;
            aproachMagnitud = direction.magnitude;
        }else{
            MoveToward();
            aproachMagnitud = direction.magnitude;
        }
    
    }
    
    public virtual void MoveToward(){
        Vector3 direction = GetPlayerDirection();
        if(direction.magnitude > myData.AttackRange)
            {
                isAttack = false;
                isRun = true;
                rbEnemy.AddForce(direction.normalized * myData.Speed, ForceMode.Impulse);
                Debug.Log("isRun: "+ isRun);
            }
            else
            {
                isAttack = true;
                isRun = false;
                rbEnemy.velocity= Vector3.zero;
                flagAttack=true;
            }    
        if(flagAttack){
            timeAttacking -= Time.deltaTime;
           
            rbEnemy.velocity= Vector3.zero;
            if(timeAttacking<=0){
                timeAttacking = timeAttack;
                flagAttack=false;
                
            }
        }
    }
    protected void LookAtPlayer(){
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
                        Debug.Log("Mataste un Leader");
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
