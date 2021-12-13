using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerCharacterController : MonoBehaviour
{
    //DESING DATA [CANDIDATO A OBJETO SERIALIZADO]
    [SerializeField] private int lifePlayer = 100;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float Gravity = -9.81f;
    [SerializeField] private List<GameObject> guns;

    //RUNTIME DATA
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Transform cam;
    [SerializeField] private float mouseSensitivity = 2f;
    private int indexGuns = 0;
    private int SelectGun=0;

    //PRIVATE COMPONENTS REFERENCE
    [SerializeField] private Animator animPlayer;
    private CharacterController cc;
    private GunController GunCtrl;

    //EVENTS
    public static event Action onDeath;
    public static event Action<int> onLivesChange;
    public static event Action<int> onGunChanges;
    [SerializeField] private UnityEvent OnTouchBox;
    public static event Action<bool> onRun;
    private void Awake()
    {
        onGunChanges?.Invoke(indexGuns);
        
    }
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        animPlayer.SetBool("isRun", false);
        animPlayer.SetBool("isRight", false);
        animPlayer.SetBool("isLeft", false);
        onLivesChange?.Invoke(lifePlayer);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GunCtrl = guns[0].GetComponent<GunController>();
        
    }
    void Update()
    {
        //MOVER
        Move();
        //ROTAR
        Rotate();
        //SALTAR
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            Debug.Log("salto");
            // animPlayer.SetBool("isRun", false);
            animPlayer.SetTrigger("jump");
            velocity.y = Mathf.Sqrt(-5f * Gravity);
        }
        //GOLPEAR
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     animPlayer.SetBool("isPunch", true);
        // }
        // if (Input.GetKeyUp(KeyCode.F))
        // {
        //     animPlayer.SetBool("isPunch", false);
        // }
        //APLICAR GRAVEDAD
        velocity.y += Gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        AnimHorizontal();
        ChangeGun();
    }


    public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (cc.isGrounded){
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                animPlayer.SetBool("isRun", true);
                cc.Move(moveDir.normalized * speed * Time.deltaTime);
                onRun?.Invoke(true);
            }
            else
            {
                animPlayer.SetBool("isRun", false);
                onRun?.Invoke(false);
            }
        } else{
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                animPlayer.SetBool("isRun", true);
                cc.Move(moveDir.normalized * 5f * Time.deltaTime);
            }
            else
            {
                animPlayer.SetBool("isRun", false);
            }
        }
        

        animPlayer.gameObject.transform.position = transform.position;
        animPlayer.gameObject.transform.rotation = transform.rotation;
 
    }
    private void AnimHorizontal(){
      if (cc.isGrounded){
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
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lifePlayer--;
            // Destroy(collision.gameObject);
            onLivesChange?.Invoke(lifePlayer);
            if (lifePlayer == 0)
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
            lifePlayer--;
            onLivesChange?.Invoke(lifePlayer);
            Debug.Log("golpe");
            if(lifePlayer < 1)
            {
                Debug.Log("GAME OVER");
                onDeath?.Invoke();
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("BulletAlien"))
            {
                lifePlayer-=3;
                onLivesChange?.Invoke(lifePlayer);
                Destroy(other.gameObject);
                if(lifePlayer< 1){
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
            lifePlayer++;
            Debug.Log("vida + 1");
            OnTouchBox?.Invoke();
            onLivesChange?.Invoke(lifePlayer);
        }
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
    

    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ESTOY EN EL PISO");
        }
    }
    */

}