using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textLife;
    [SerializeField] private Text textAmo;
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateInfoUI(){
        int[] playerInfo = playerController.GetPlayerInfo();
        textLife.text = playerInfo[0] + "%";
    }
}
