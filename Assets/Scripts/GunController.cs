using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Vector3 spawnPosition= new Vector3(0.79f,1.718f,-1.934f);
    public GameObject prefabBullet;
    private Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.4f);
    
    public float coolDown = 0.5f;
    public float timePass=0;
    public bool isShoot= false;
    public bool isDuplicated= false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isShoot){
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
        
        if(Input.GetKeyDown(KeyCode.Space) && !isDuplicated){
            prefabBullet.transform.localScale += scaleChange;
            isDuplicated=true;
            Debug.Log(prefabBullet.transform.localScale);
            Debug.Log(isDuplicated);
        }
        if(Input.GetKeyDown(KeyCode.Space) && isDuplicated){
            prefabBullet.transform.localScale -= scaleChange;
            isDuplicated=false;
            Debug.Log(prefabBullet.transform.localScale);
            Debug.Log(isDuplicated);
        }
    }
}
