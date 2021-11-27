using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textLife;
    [SerializeField] private Text textAmo;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private List<GameObject> gunsImages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfoUI();
    }
    private void UpdateInfoUI(){
        int[] playerInfo = playerController.GetPlayerInfo();
        textLife.text = playerInfo[0] + "%";
        textAmo.text = playerInfo[1] + " "; 
        ChangeGunImageUI(playerInfo[2]);
    }
    private void ChangeGunImageUI(int indexGun){
        for (int i = 0; i < gunsImages.Count; i++)
            {
                if(i == indexGun)
                {
                    gunsImages[i].SetActive(true);
                }
                else
                {
                    gunsImages[i].SetActive(false);
                }

            }
    }
}
