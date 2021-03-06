using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition= new Vector3(0.79f,1.718f,-1.934f);
    [SerializeField] private GameObject bulletPosition;
    [SerializeField] private GameObject prefabBullet;
    private Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.4f);
    
    [SerializeField] private float coolDown = 0.5f;
    [SerializeField] private float reloadTime = 2.5f;
    [SerializeField] private float timePass=0;
    [SerializeField] private float timeReloadPass=0;
    [SerializeField] private bool isShoot= false;
    [SerializeField] private int bulletQuantity = 100;
    private int bulletCharge;
    [SerializeField] private int indexGun;

    // [SerializeField] private int indexGun;
    private bool reloadFlag = false;
  
    private GameObject player;
   
    // eventos
    public static event Action<int> onAmoChange;
    [SerializeField] private UnityEvent onReloadStart;
    [SerializeField] private UnityEvent onReloadEnd;


    private void Awake()
    {
        bulletCharge = bulletQuantity;
        onAmoChange?.Invoke(bulletCharge);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
        PlayerCharacterController.onGunChanges+= OnGunChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotationPlayer= player.transform.rotation;
        if(Input.GetMouseButtonDown(0) && !isShoot && bulletCharge>0){
            isShoot=true;
            Instantiate(prefabBullet,bulletPosition.transform.position , Quaternion.Euler(rotationPlayer.eulerAngles.x,rotationPlayer.eulerAngles.y,rotationPlayer.eulerAngles.z));
            bulletCharge--;
            onAmoChange?.Invoke(bulletCharge);
        }
        if(bulletCharge < 1){
            timeReloadPass+=Time.deltaTime;
            reloadFlag = true;
            onReloadStart?.Invoke();
        }
        if(timeReloadPass > reloadTime){
            reloadFlag=false;
            timeReloadPass=0;
            onReloadEnd?.Invoke();
            bulletCharge = bulletQuantity;
            onAmoChange?.Invoke(bulletCharge);
        }
        if(isShoot){
            timePass+=Time.deltaTime;
        }
        if(timePass > coolDown){
            isShoot=false;
            timePass=0;
        }
    }
    private void OnGunChangeHandler(int indexGunchange){
        if(indexGunchange == indexGun){
            Debug.Log("el indexgunchange es :" + indexGunchange);
            Debug.Log("el indexgun del arma es :" + indexGun);
            onAmoChange?.Invoke(bulletCharge);
        }
    }
    public bool GetReloadFlag(){
        return reloadFlag;
    }
    public int GetBulletCharge(){
        return bulletCharge;
    }
}
