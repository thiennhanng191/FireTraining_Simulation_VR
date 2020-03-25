using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAreaEmission : MonoBehaviour
{
    public ParticleSystem checkpointFire;
    public ParticleSystem smokeArea; 
    

    // Start is called before the first frame update
    void Start()
    {
        smokeArea = smokeArea.GetComponent<ParticleSystem>();
        checkpointFire = checkpointFire.GetComponent<ParticleSystem>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        var checkPointFireEmission = checkpointFire.emission;
        var smokeAreaEmission = smokeArea.emission;
        
        if (checkPointFireEmission.enabled)
        {
            smokeAreaEmission.enabled = true; 
        }
        
    }

}
