using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition= new Vector3(0.79f,1.718f,-1.934f);
    [SerializeField] private GameObject bulletPosition;
    [SerializeField] private GameObject prefabBullet;
    private Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.4f);
    
    [SerializeField] private float coolDown = 0.5f;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private float timePass=0;
    [SerializeField] private float timeReloadPass=0;
    [SerializeField] private bool isShoot= false;
    [SerializeField] private int bulletQuantity = 100;
    [SerializeField] private int bulletCharge;
    private bool reloadFlag = false;
  
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bulletCharge = bulletQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotationPlayer= player.transform.rotation;
        if(Input.GetMouseButtonDown(0) && !isShoot && bulletCharge>0){
            isShoot=true;
            Instantiate(prefabBullet,bulletPosition.transform.position , Quaternion.Euler(rotationPlayer.eulerAngles.x,rotationPlayer.eulerAngles.y,rotationPlayer.eulerAngles.z));
            bulletCharge--;
        }
        if(bulletCharge <= 0){
            timeReloadPass+=Time.deltaTime;
            reloadFlag = true;
        }
        if(timeReloadPass > reloadTime){
            Debug.Log("reloadFlag: " + reloadFlag);
            reloadFlag=false;
            timeReloadPass=0;
            bulletCharge = bulletQuantity;
        }
        if(isShoot){
            timePass+=Time.deltaTime;
        }
        if(timePass > coolDown){
            isShoot=false;
            timePass=0;
        }
                
    }
    public bool GetReloadFlag(){
        return reloadFlag;
    }
    public int GetBulletCharge(){
        return bulletCharge;
    }
}
