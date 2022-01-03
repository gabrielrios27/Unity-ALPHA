using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text textLife;
    [SerializeField] private Text textAmo;
    [SerializeField] private Text textGetInfo;
    [SerializeField] private Text pressE;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text gameOverPressEscText;
    [SerializeField] private PlayerCharacterController playerCharacterController;
    [SerializeField] private List<GameObject> gunsImages;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerCharacterController.onLivesChange+= OnLivesChangeHandler;
        PlayerCharacterController.onGunChanges+= OnGunChangesHandler;
        GunController.onAmoChange+= OnAmoChangeHandler;
    }
    private void Start(){
         PlayerCharacterController.onCloseProjector+= CloseProjectHandler;
         PlayerCharacterController.onFarAwayProjector+= FarAwayProjectorHandler;
         PlayerCharacterController.onPressE+= OnPressEHandler;
         PlayerCharacterController.onDeath+= OnGameOverHandler;
    }
    private void OnDestroy(){
        PlayerCharacterController.onLivesChange-= OnLivesChangeHandler;
        PlayerCharacterController.onGunChanges-= OnGunChangesHandler;
        GunController.onAmoChange-= OnAmoChangeHandler;
        PlayerCharacterController.onCloseProjector-= CloseProjectHandler;
        PlayerCharacterController.onFarAwayProjector-= FarAwayProjectorHandler;
        PlayerCharacterController.onPressE-= OnPressEHandler;
        PlayerCharacterController.onDeath-= OnGameOverHandler;
    }
    private void OnGameOverHandler(){
        gameOverText.text = "Game Over";
        gameOverPressEscText.text = "Presiona Esc para volver al Presente";
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
    private void CloseProjectHandler(){
        pressE.text = "Presiona E";
    }
    private void FarAwayProjectorHandler(){
        pressE.text = "";
    }
    private void OnPressEHandler(){
        textGetInfo.text = "Conseguiste la Información! Presiona Esc para volver al presente";
    }
}
