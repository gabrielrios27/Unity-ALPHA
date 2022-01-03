using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienWithNavigation : AlienShooter
{
    private NavMeshAgent enemyAgent;
    private float speedPatrolNavMesh = 2;
    private float speedChaseNavMesh = 6;
    [SerializeField] bool leader;
    void Start()
    {
        enemyAgent= GetComponent<NavMeshAgent>();
        rbEnemy = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        rbEnemy = GetComponent<Rigidbody>();
        animEnemy = gameObject.transform.GetChild(0).GetComponent<Animator>();
        armorEnemy = myData.Armor;
        timeAttacking = timeAttack;
        attackingRangeOfView= rangeOfView+30;
    }
    void Update()
    {
        animEnemy.SetBool("isRun", isRun);
        animEnemy.SetBool("isAttack", isAttack);
        animEnemy.SetBool("isPatrolling", isPatrolling);
     }
    public override void HuntLookDied(){
        isPatrolling = false;
        GetComponent<NavMeshAgent>().speed = speedChaseNavMesh;
        LookAtPlayer();
        MoveToward();
    }
    public override void MoveToward()
    {
        isLeader=leader;
        Vector3 direction = GetPlayerDirection();
        if(direction.magnitude > myData.AttackRange && !flagAttack)
            {
                isAttack = false;
                isRun = true;
                enemyAgent.destination = player.transform.position;
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
            if(timeAttacking<=0){
                timeAttacking = timeAttack;
                flagAttack=false;
            }
        }

    }
    public override void Patrol()
    {
        GetComponent<NavMeshAgent>().speed = speedPatrolNavMesh;
        isPatrolling = true;
        isRun = false;
        isAttack = false;

        Vector3 deltaVector = waypoints[currentIndex].position - transform.position;
        enemyAgent.destination = waypoints[currentIndex].position;

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
    
}
