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

    // Variables para Patrullaje
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed = 5;
    [SerializeField] float rotationSpeed= 2;
    [SerializeField] float minimumDistance = 1;
    [SerializeField] GameObject character;
    [SerializeField] float rangeOfView = 15;
    private bool iSeeTheCharacter = false;
    private int currentIndex = 0;
    private bool goBack = false;

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
        if (Vector3.Distance(transform.position, character.transform.position) <= rangeOfView)
        {
            iSeeTheCharacter = true;
        }
        else
        {
            iSeeTheCharacter = false;
        }
        if (iSeeTheCharacter)
        {
            // ChaseCharacter();
            HuntLookDied();
        }
        else
        {
            Patrol();
        }

        // --------------------
        // HuntLookDied();
    }
    private void Patrol()
    {
        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        Vector3 direction = deltaVector.normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;

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
    private void ChaseCharacter()
    {
        Debug.Log("VEO AL PLAYER, ATACAR!!");

        Vector3 direction = (character.transform.position - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;
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
    // -------------------------------------

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
