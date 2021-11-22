using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;
    private int indexCamera = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            indexCamera++;
            if (indexCamera == cameras.Count)
            {
                indexCamera = 0;
            }
            SwitchCameras(indexCamera);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            indexCamera--;
            if (indexCamera < 0)
            {
                indexCamera = cameras.Count - 1;
            }
            SwitchCameras(indexCamera);
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
