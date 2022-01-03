using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LandingController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUserName;
   

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEndInputUsername()
    {
        ProfileManager.instance.SetPlayerName(inputUserName.text);
        Debug.Log("el nombre es: " + ProfileManager.instance.GetPlayerName());
    }

  
    public void OnChangeSlider(float newValue)
    {
        Debug.Log(newValue);
    }

    public void OnClickPlay()
    {
        Debug.Log("A JUGAR!!");
        SceneManager.LoadScene("Level1Present");
    }
    public void OnClickQuit()
    {
        Debug.Log("Salir");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
