using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLifeController : MonoBehaviour
{
    // Start is called before the first frame update
   public void OnTouchBoxHandler(){
        transform.position= new Vector3(Random.Range(-22f, 3f),0.6f,Random.Range(-14f, 12f));
        transform.rotation= Quaternion.Euler(0,Random.Range(0, 360),0);
        Debug.Log("Evento Unity OnTouch - llamado por : PlayerController - recibido por BoxLifeController");
   }

}
