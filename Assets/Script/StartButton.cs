using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public ParticleSystem outsideFire;
    public ParticleSystem outsideFire2;

    public ParticleSystem centerFire;
    public ParticleSystem sparks;
    public ParticleSystem smoke;
    public Light lightFire;

    public AudioSource fireAudio;

    public GameObject instructionPanelFirst;
    public GameObject startPanel;

    public GameObject checkFire;

    public GameObject winGame; 

    void Start()
    {
        //hose = GetComponent<ParticleSystem>();
        outsideFire = outsideFire.GetComponent<ParticleSystem>();
        centerFire = centerFire.GetComponent<ParticleSystem>();
        sparks = sparks.GetComponent<ParticleSystem>();
        smoke = smoke.GetComponent<ParticleSystem>();
        lightFire = lightFire.GetComponent<Light>();

        fireAudio = outsideFire.GetComponent<AudioSource>();
    }

        public void StartButtonHandling()
    {
        var outsideFireEmission = outsideFire.emission;
        var centerFireEmission = centerFire.emission;
        var sparksEmission = sparks.emission;
        var smokeEmission = smoke.emission;

        outsideFireEmission.enabled = true;
        centerFireEmission.enabled = true;
        sparksEmission.enabled = true;
        smokeEmission.enabled = true;

        fireAudio.Play();

        lightFire.GetComponent<Light>().enabled = true;
        lightFire.GetComponent<flickering>().enabled = true;

        instructionPanelFirst.SetActive(true);
        startPanel.SetActive(false);

        checkFire.GetComponent<CheckFireEmitted>().enabled = true;
        outsideFire2.GetComponent<FireNextIgnition>().enabled = true;

        winGame.GetComponent<WinGame>().enabled = true; 
        
    }
}
