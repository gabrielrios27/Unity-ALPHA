using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyBox : MonoBehaviour
{
    public string namePlayer = "Box Birdy";
    public float lifePlayer = 1000;
    public float changeLife = 5;
    public float speedCuration = 10;
    public float speedPlayer = 5;
    public Vector3 position1 = new Vector3(-1.65f, -0.219f, -6.22f);
    public Vector3 changePosition = new Vector3(0, 0.01f, 0.1f);
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = position1;
  
    }

    // Update is called once per frame
    void Update()
    {
        move();
        // cure();
        damage();
    }
    void move(){
        transform.position += changePosition * speedPlayer * Time.deltaTime;
    }
    void cure(){
        lifePlayer += changeLife * speedCuration * Time.deltaTime;
    }
    void damage(){
        lifePlayer -= changeLife * speedCuration * Time.deltaTime;
    }
}
