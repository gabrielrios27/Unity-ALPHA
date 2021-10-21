using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   

    public float speedBullet = 10f;
    public Vector3 directionBullet = new Vector3(0, 0, 1f);
    public float damageBullet = 5;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedBullet * Time.deltaTime * directionBullet);
    }
}
