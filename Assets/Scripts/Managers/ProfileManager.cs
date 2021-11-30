using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
   public static ProfileManager instance;
    // Start is called before the first frame update

   [SerializeField] private string playerName;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
   
}
