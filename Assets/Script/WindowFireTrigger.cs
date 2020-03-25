using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowFireTrigger : MonoBehaviour
{
    public ParticleSystem windowOuterFire;
    public ParticleSystem windowCenterFire;
    public ParticleSystem windowSparks;
    public ParticleSystem windowFireSmoke;

    public Light lightWindowFire; 

    // Start is called before the first frame update
    void Start()
    {
        windowOuterFire = windowOuterFire.GetComponent<ParticleSystem>();
        windowCenterFire = windowCenterFire.GetComponent<ParticleSystem>();
        windowSparks = windowSparks.GetComponent<ParticleSystem>();
        windowFireSmoke = windowFireSmoke.GetComponent<ParticleSystem>();
        lightWindowFire = lightWindowFire.GetComponent<Light>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var windowFireEmission = windowOuterFire.emission;
            var windowCenterFireEmission = windowCenterFire.emission;
            var windowSparksEmission = windowSparks.emission;
            var windowFireSmokeEmission = windowFireSmoke.emission; 

            windowFireEmission.enabled = true;
            windowCenterFireEmission.enabled = true;
            windowSparksEmission.enabled = true;
            windowFireSmokeEmission.enabled = true;

            lightWindowFire.GetComponent<Light>().enabled = true;
            lightWindowFire.GetComponent<flickering>().enabled = true;
        }
    }
}
