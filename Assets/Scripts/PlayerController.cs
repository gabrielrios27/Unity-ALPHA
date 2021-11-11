using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private int lifePlayer = 5;
    // [SerializeField] private string namePlayer = "Alpha1";
    [SerializeField] private float speedPlayer = 0.5f;
    private float cameraAxis= 0;
    [SerializeField] private Vector3 initPosition = new Vector3(4, 2, 1);

    [SerializeField] private Animator animPlayer;
   
    // Start is called before the first frame update
    void Start()
    {
        animPlayer.SetBool("isRun", false);
    }

    // Update is called once per frame
    void Update()
    {   
        RotatePlayer();
        Move();
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
}
