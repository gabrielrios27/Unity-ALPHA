using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShooter : AlienEnemy
{
    public override void MoveToward()
    {
        isLeader=true;
        isRun = false;
        isPatrolling = true;
        Vector3 direction = GetPlayerDirection();
        if(direction.magnitude > myData.AttackRange)
        {
            isAttack = false;
            ChaseCharacter();
        }
        else
        {
            isAttack = true;
            
        }
    }
    private void ChaseCharacter()
    {
        Debug.Log("VEO AL PLAYER, ATACAR!!");

        Vector3 direction = (character.transform.position - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * speedPatrol * Time.deltaTime;
    }
}
