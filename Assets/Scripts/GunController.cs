using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Vector3 spawnPosition= new Vector3(0.79f,1.718f,-1.934f);
    public GameObject gunPosition;
    public GameObject prefabBullet;
    private Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.4f);
    
    public float coolDown = 0.5f;
    public float timePass=0;
    public bool isShoot= false;
  
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotationPlayer= player.transform.rotation;
        if(Input.GetMouseButtonDown(0) && !isShoot){
            isShoot=true;
            Instantiate(prefabBullet,gunPosition.transform.position , Quaternion.Euler(rotationPlayer.eulerAngles.x,rotationPlayer.eulerAngles.y-180,rotationPlayer.eulerAngles.z));
            Debug.Log(player.transform.rotation.eulerAngles);
         }
        if(isShoot){
            timePass+=Time.deltaTime;
        }
        if(timePass > coolDown){
            isShoot=false;
            timePass=0;
        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            prefabBullet.transform.localScale += prefabBullet.transform.localScale;
        }
        
    }
}
