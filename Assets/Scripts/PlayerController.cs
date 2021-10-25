using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 spawnPosition= new Vector3(0.11f,1.7207f,-1.8467f);
    public GameObject prefabBullet;
    public float coolDown = 3f;
    public float timePass=0;
    public bool isShoot= false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isShoot){
            isShoot=true;
            Instantiate(prefabBullet,spawnPosition,prefabBullet.transform.rotation);
        }
        if(isShoot){
            timePass+=Time.deltaTime;
        }
        if(timePass > coolDown){
            isShoot=false;
            timePass=0;
        }
    }
}
