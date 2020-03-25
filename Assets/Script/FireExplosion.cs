using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion : MonoBehaviour
{
    public ParticleSystem outerFire;

    private ParticleSystem.MainModule outerFireMain;

    public GameObject glassWindow;

    public GameObject explosionTrigger; 

    public ParticleSystem explosion;
    public ParticleSystem explosionComponent1;
    public ParticleSystem explosionComponent2;
    public ParticleSystem explosionComponent3;
    public ParticleSystem explosionComponent4;


    public AudioSource explosionSound; 

    // Start is called before the first frame update
    void Start()
    {
        //outerFire = outerFire.GetComponent<ParticleSystem>(); 
        outerFireMain = outerFire.main;
        //explosion = explosion.GetComponent<ParticleSystem>();
        explosionSound = explosion.GetComponent<AudioSource>(); 

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(StartExplosion());
        }
    }

    IEnumerator StartExplosion()
    {
        //var emitParams = outerFire.EmitParams().start;
        /* var explosionEmission = explosion.Play(;
        var explosionComponent1Emission = explosionComponent1.emission;
        var explosionComponent2Emission = explosionComponent2.emission;
        var explosionComponent3Emission = explosionComponent3.emission;
        var explosionComponent4Emission = explosionComponent3.emission;

        explosionEmission.enabled = true;
        explosionComponent1Emission.enabled = true;
        explosionComponent2Emission.enabled = true;
        explosionComponent3Emission.enabled = true;
        explosionComponent4Emission.enabled = true;
        */
        explosionTrigger.SetActive(false);

        explosion.gameObject.SetActive(true);
        explosion.Play();
        explosionComponent1.gameObject.SetActive(true);
        explosionComponent1.Play();
        explosionComponent2.gameObject.SetActive(true);
        explosionComponent2.Play();
        explosionComponent3.gameObject.SetActive(true);
        explosionComponent3.Play();
        explosionComponent4.gameObject.SetActive(true);
        explosionComponent4.Play();


        explosionSound.Play();

        outerFireMain.startSize = new ParticleSystem.MinMaxCurve(10f,12f);
        outerFireMain.startSpeed = new ParticleSystem.MinMaxCurve(1f, 1.2f);
        glassWindow.GetComponent<Breakable>().BreakFunction();

        yield return new WaitForSeconds(0.1f);

        outerFireMain.startSize = 1.8f;
        outerFireMain.startSpeed = -0.75f; 
    }
}
