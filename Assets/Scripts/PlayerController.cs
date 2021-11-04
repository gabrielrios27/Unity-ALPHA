using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private int lifePlayer = 5;
    // [SerializeField] private string namePlayer = "Alpha1";
    [SerializeField] private float speedPlayer = 0.5f;
    private float cameraAxis= 180;
    [SerializeField] private Vector3 initPosition = new Vector3(4, 2, 1);
    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.Translate(speedPlayer * Time.deltaTime * new Vector3(-ejeHorizontal, 0, -ejeVertical));
    }
    private void RotatePlayer(){
        cameraAxis += Input.GetAxis("Mouse X");
        Quaternion angle = Quaternion.Euler(0, cameraAxis,0);
        transform.localRotation = angle;
    }
}
