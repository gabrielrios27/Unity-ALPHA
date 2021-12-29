using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textLife;
    [SerializeField] private Text textAmo;
    [SerializeField] private PlayerCharacterController playerCharacterController;
    [SerializeField] private List<GameObject> gunsImages;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerCharacterController.onLivesChange+= OnLivesChangeHandler;
        PlayerCharacterController.onGunChanges+= OnGunChangesHandler;
        GunController.onAmoChange+= OnAmoChangeHandler;
    }
    private void OnDestroy(){
        PlayerCharacterController.onLivesChange-= OnLivesChangeHandler;
        PlayerCharacterController.onGunChanges-= OnGunChangesHandler;
        GunController.onAmoChange-= OnAmoChangeHandler;
    }
    private void OnGunChangesHandler(int indexGun){
        for (int i = 0; i < gunsImages.Count; i++)
            {
                if(i == indexGun)
                { 
                    gunsImages[i].SetActive(true);
                    Debug.Log("Evento OnGunChanges - llamado por : PlayerCharacterController - recibido por HUDController");
                }
                else
                {
                    gunsImages[i].SetActive(false);
                }
            }
    }
    private void OnAmoChangeHandler(int amo){
        textAmo.text = amo + " ";
        Debug.Log("Evento OnAmoChange - llamado por : GunController - recibido por HUDController");
    }
    private void OnLivesChangeHandler(int armor){
        textLife.text = armor + "%";
        Debug.Log("Evento OnLivesChange - llamado por : PlayerCharacterController - recibido por HUDController");
    }
}
