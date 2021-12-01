using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AlienData", menuName = "Alien Data")]
public class AlienData : ScriptableObject
{
    [SerializeField] private string alienName;
    [SerializeField] private int hp;
    [SerializeField] private int armor;
    [SerializeField] private float speed;
    [SerializeField] private float speedToLook;
    [SerializeField] private float damageHit;
    [SerializeField] private float attackRange;
    //GETTER
    public string AlienName { 
        get
        {
            return alienName;
        } 
    }

    public int HP
    {
        get
        {
            return hp;
        }
    }

    public int Armor
    {
        get
        {
            return armor;
        }
    }
    public float Speed
    {
        get
        {
            return speed;
        }
    }
    public float SpeedToLook
    {
        get
        {
            return speedToLook;
        }
    }
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }
}
