using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetTrigger : MonoBehaviour
{
    public ParticleSystem faucetWater;
    public ParticleSystem faucetWaterSparkles;
    // Start is called before the first frame update
    void Start()
    {
        faucetWater = faucetWater.GetComponent<ParticleSystem>();
        faucetWaterSparkles = faucetWaterSparkles.GetComponent<ParticleSystem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            var faucetWaterEmission = faucetWater.emission;
            var faucetWaterSparklesEmission = faucetWaterSparkles.emission;

            faucetWaterEmission.enabled = true;
            faucetWaterSparklesEmission.enabled = true; 
        }
    }
}
