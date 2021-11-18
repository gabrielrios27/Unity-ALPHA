using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAlien : MonoBehaviour
{
    [SerializeField] private float speedBullet = 15f;
    [SerializeField] private Vector3 directionBullet = new Vector3(0, 0, 1f);
    // [SerializeField] private float damageBullet = 6;
    [SerializeField] private float lifeBullet = 3f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        destroyBullet(directionBullet);
    }
    private void MoveBullet(Vector3 direction){
        transform.Translate(speedBullet * Time.deltaTime * direction);
    }
    private void destroyBullet(Vector3 direction){
        lifeBullet -= Time.deltaTime;
        if(lifeBullet > 0){
            MoveBullet(direction);
        }else{
            Destroy(gameObject);
        }
    }
}
