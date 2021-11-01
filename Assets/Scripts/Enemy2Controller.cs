using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    // Start is called before the first frame update   
    public float speedEnemy2 = 3;
    public float lifeEnemy2 = 7f;
    public Vector3 directionEnemy2 = Vector3.back;
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
        
    }
   
    private void destroyEnemy(){
        lifeEnemy2 -= Time.deltaTime;
        if(lifeEnemy2 > 0){
            MoveToward();
        }else{
            Destroy(gameObject);
        }
    }
    private void MoveToward(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += speedEnemy2 * Time.deltaTime * direction;
    }
    private void LookAtPlayer(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speedToLook * Time.deltaTime); 
    }
}
