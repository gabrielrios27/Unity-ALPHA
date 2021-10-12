using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyBox : MonoBehaviour
{
    public string namePlayer = "Box Birdy";
    public Vector3 position1 = new Vector3(-1.65f, -0.219f, -6.22f);
  

    public Vector3 changePosition = new Vector3(0, 0.001f, 0.01f);
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = position1;
  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += changePosition;
   
        
    }
}
