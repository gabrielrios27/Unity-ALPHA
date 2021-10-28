using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lifePlayer = 5;
    public string namePlayer = "Alpha1";
    public float speedPlayer = 0.5f;
   
    public Vector3 initPosition = new Vector3(4, 2, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        float ejeHorizontal = Input.GetAxisRaw("Horizontal");
        float ejeVertical = Input.GetAxisRaw("Vertical");
        transform.Translate(speedPlayer * Time.deltaTime * new Vector3(-ejeHorizontal, 0, -ejeVertical));
    }
}
