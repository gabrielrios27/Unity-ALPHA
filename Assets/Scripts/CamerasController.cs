using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private GameObject[] cameras;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
            cameras[2].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            cameras[2].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(false);
            cameras[2].SetActive(true);
        }
    }
}
