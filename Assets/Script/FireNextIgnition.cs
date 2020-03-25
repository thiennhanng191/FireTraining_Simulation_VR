using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNextIgnition : MonoBehaviour
{
    //public ParticleSystem hose;
    public ParticleSystem outsideFire;
    public ParticleSystem centerFire;
    public ParticleSystem sparks;
    public ParticleSystem smoke;
    public Light lightFire;

    public ParticleSystem outsideFirePrevious;
    public ParticleSystem outsideFireNext;

    public AudioSource fireAudio; 
    
    // Start is called before the first frame update
    void Start()
    {
        //hose = GetComponent<ParticleSystem>();
        outsideFire = outsideFire.GetComponent<ParticleSystem>();
        centerFire = centerFire.GetComponent<ParticleSystem>();
        sparks = sparks.GetComponent<ParticleSystem>();
        smoke = smoke.GetComponent<ParticleSystem>();
        lightFire = lightFire.GetComponent<Light>();

        fireAudio = outsideFire.GetComponent<AudioSource>(); 

        StartCoroutine(FireStart());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator FireStart()
    {
        while (outsideFirePrevious.emission.enabled)
        {
            Debug.Log("checkpoint");
            yield return new WaitForSeconds(10f);

            //if (outsideFirePrevious.emission.enabled == true)
            //{

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

                outsideFireNext.GetComponent<FireNextIgnition>().enabled = true;

                RenderSettings.fogDensity += 0.001f;


        }



    }

}
