using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private int lifePlayer = 5;
    // [SerializeField] private string namePlayer = "Alpha1";
    [SerializeField] private float speedPlayer = 0.5f;
    [SerializeField] private int armorPlayer = 100;
    [SerializeField] private Animator animPlayer;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private List<GameObject> guns;
    // [SerializeField] private GunController gumCtr;
    private float cameraAxis= 0;
    private int indexGuns = 0;
    private Rigidbody rb;
    private bool isGrounded = true;
    private int[] PlayerInfo = {0, 0, 0};
    private GunController GunCtrl;
    private int SelectGun=0;
 
    // eventos 
    public static event Action onDeath;
    public static event Action<int> onLivesChanges;
    public static event Action<int> onGunChanges;
    [SerializeField] private UnityEvent OnTouchBox;
   
    // Start is called before the first frame update
    void Start()
    {
        animPlayer.SetBool("isRun", false);
        animPlayer.SetBool("isRight", false);
        animPlayer.SetBool("isLeft", false);
        rb = GetComponent<Rigidbody>();
        GunCtrl = guns[0].GetComponent<GunController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        onLivesChanges?.Invoke(armorPlayer);
        onGunChanges?.Invoke(indexGuns);
    }

    // Update is called once per frame
    void Update()
    {   
        RotatePlayer();
        Move();
        Jump();
        AnimHorizontal();
        ChangeGun();
        UpdatePlayerInfo();
        // ReloadAnim();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            armorPlayer--;
            onLivesChanges?.Invoke(armorPlayer);
            Debug.Log("golpe");
            if(armorPlayer < 0)
            {
                onDeath?.Invoke();
                Debug.Log("GAME OVER");
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHand"))
        {
            armorPlayer--;
            onLivesChanges?.Invoke(armorPlayer);
            Debug.Log("golpe");
            if(armorPlayer < 1)
            {
                Debug.Log("GAME OVER");
                onDeath?.Invoke();
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("BulletAlien"))
            {
                armorPlayer-=3;
                onLivesChanges?.Invoke(armorPlayer);
                Destroy(other.gameObject);
                if(armorPlayer< 1){
                     Debug.Log("GAME OVER");
                     onDeath?.Invoke();
                     Destroy(gameObject);
                }
  
            }
    } 
     private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BoxLife"))
        {
            armorPlayer++;
            Debug.Log("vida + 1");
            OnTouchBox?.Invoke();
            onLivesChanges?.Invoke(armorPlayer);
        }
    }
    private void Move()
    {
        float ejeHorizontal = Input.GetAxisRaw("Horizontal");
        float ejeVertical = Input.GetAxisRaw("Vertical");
        Vector3 angleStay = transform.localRotation.eulerAngles;
        if (ejeHorizontal != 0 || ejeVertical != 0) {

            animPlayer.SetBool("isRun", true);
            Vector3 direction = new Vector3(ejeHorizontal, 0, ejeVertical);
            transform.Translate(speedPlayer * Time.deltaTime * direction);
        }
        else
        {
            animPlayer.SetBool("isRun", false);
        }
        
    }
    private void RotatePlayer(){
        cameraAxis += Input.GetAxis("Mouse X");
        Quaternion angle = Quaternion.Euler(0, cameraAxis,0);
        transform.localRotation = angle;
    }
    private void Jump()
    {
        isGrounded=IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(0,1 * jumpForce,0);
                animPlayer.SetTrigger("jump");
            }
        }
  
    }
    private void AnimHorizontal(){
      
       if(Input.GetKey(KeyCode.D)){
           if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
                animPlayer.SetBool("isRun", true);
                animPlayer.SetBool("isRight", false);
           }else{
                animPlayer.SetBool("isRight", true);
           }
       }else{
           animPlayer.SetBool("isRight", false);
       }

       if(Input.GetKey(KeyCode.A)){
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
                animPlayer.SetBool("isRun", true);
                animPlayer.SetBool("isLeft", false);
           }else{
                animPlayer.SetBool("isLeft", true);
           }
            
       }else{
           animPlayer.SetBool("isLeft", false);
       }
    }
    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position + new Vector3(0,0.3f,0), Vector3.down, 0.5f, groundLayer))
        {
            return true;
        }
        else return false;
    }
    private void SwitchGuns(int index)
    {
         for (int i = 0; i < guns.Count; i++)
            {
                if(i == index)
                {
                    SelectGun = index;
                    guns[i].SetActive(true);
                    GunCtrl = guns[i].GetComponent<GunController>();
                }
                else
                {
                    guns[i].SetActive(false);
                }

            }
    }
    private void ChangeGun()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            indexGuns++;
            if (indexGuns == guns.Count)
            {
                indexGuns = 0;
            }
            SwitchGuns(indexGuns);
            onGunChanges?.Invoke(indexGuns);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            indexGuns--;
            if (indexGuns < 0)
            {
                indexGuns = guns.Count - 1;
            }
            SwitchGuns(indexGuns);
            onGunChanges?.Invoke(indexGuns);
        }
    }
    private void UpdatePlayerInfo(){
        PlayerInfo[0] = armorPlayer;
        int v = GunCtrl.GetBulletCharge();
        PlayerInfo[1] = v;
        PlayerInfo[2] = SelectGun;
    }
    public int[] GetPlayerInfo(){
        return PlayerInfo;
    }
    private void ReloadAnim(){
        animPlayer.SetBool("isReload", GunCtrl.GetReloadFlag());
    }
    public void OnReloadStartAnim(){
        animPlayer.SetBool("isReload", true);
        Debug.Log("Evento Unity OnReloadStart - llamado por : GunController - recibido por PlayerCharacterController");
     }
    public void OnReloadEndAnim(){
        animPlayer.SetBool("isReload", false);
         Debug.Log("Evento Unity OnReloadEnd - llamado por : GunController - recibido por PlayerCharacterController");
    }
}
