using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostGlobalController : MonoBehaviour
{
    private PostProcessVolume globalVolume;

    // Start is called before the first frame update
    private void Awake()
    {
        globalVolume = GetComponent<PostProcessVolume>();
        PlayerCharacterController.onRun += StatusLensDistortionEffect;
    }

    void Start()
    {
        StatusLensDistortionEffect(false);
    }

    public void StatusLensDistortionEffect(bool status)
    {
        Debug.Log("corre: "+ status);
        LensDistortion lensFX;
        // ColorGrading colorFX;
        globalVolume.profile.TryGetSettings(out lensFX);
        lensFX.active = status;
    }

}
