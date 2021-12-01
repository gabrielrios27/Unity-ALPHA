using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShooter : AlienEnemy
{
    public override void MoveToward()
    {
        isLeader=true;
        isRun = false;
        Vector3 direction = GetPlayerDirection();
        if(direction.magnitude > myData.AttackRange)
        {
            isAttack = false;
        }
        else
        {
            isAttack = true;
        }
    }
}
