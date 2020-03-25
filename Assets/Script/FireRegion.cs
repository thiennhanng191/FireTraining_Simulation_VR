using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FireRegion : MonoBehaviour
{
    public ParticleSystem ongoingFire;

    public Image healthBar;

    public float decreasingAmount = 0.05f;

    void Start()
    {
        ongoingFire = ongoingFire.GetComponent<ParticleSystem>(); 
    }

     void OnTriggerEnter(Collider other)
    {
        var ongoingFireEmission = ongoingFire.emission;
        if (ongoingFireEmission.enabled == true)
        {
            if (other.tag == "Player")
            {
                healthBar.fillAmount -= decreasingAmount;
            }
        }
    }
}
