using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private int lifePlayer = 5;
    // [SerializeField] private string namePlayer = "Alpha1";
    [SerializeField] private float speedPlayer = 0.5f;
    [SerializeField] private int armorPlayer = 5;
    private float cameraAxis= 0;
    
    [SerializeField] private Animator animPlayer;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] LayerMask groundLayer;
    private Rigidbody rb;
    private bool isGrounded = true;
   
    // Start is called before the first frame update
    void Start()
    {
        animPlayer.SetBool("isRun", false);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        RotatePlayer();
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            armorPlayer--;
            Debug.Log("golpe");
            if(armorPlayer < 0)
            {
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
            Debug.Log("golpe");
            if(armorPlayer < 0)
            {
                Debug.Log("GAME OVER");
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
            // transform.GetChild(0).localEulerAngles = new Vector3(0,angleStay.y + 40,0);
        }
        
    }
    private void RotatePlayer(){
        cameraAxis += Input.GetAxis("Mouse X");
        Quaternion angle = Quaternion.Euler(0, cameraAxis,0);
        transform.localRotation = angle;
    }
    private void Jump()
    {
        Debug.Log("Deberia saltar");
        rb.AddForce(0,1 * jumpForce,0);
    }
    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f, groundLayer))
        {
            return true;
        }
        else return false;
    }
}
