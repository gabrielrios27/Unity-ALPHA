using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CamerasController : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;
    private int indexCamera = 0;
    // eventos
    public static event Action<int> onCameraChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            indexCamera++;
            if (indexCamera == cameras.Count)
            {
                indexCamera = 0;
            }
            SwitchCameras(indexCamera);
            onCameraChange?.Invoke(indexCamera);
        }
      
    }
     private void SwitchCameras(int index)
    {
         for (int i = 0; i < cameras.Count; i++)
        {
            if(i == index)
            {
                cameras[i].SetActive(true);
            }
            else
            {
                cameras[i].SetActive(false);
            }

        }
    }
}
