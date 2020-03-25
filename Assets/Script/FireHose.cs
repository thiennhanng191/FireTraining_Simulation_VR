using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHose : MonoBehaviour
{
    public ParticleSystem fireHose; 

    // Start is called before the first frame update
    void Start()
    {
        fireHose = GetComponent<ParticleSystem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        var emission = fireHose.emission;
        emission.rateOverTime = 0f;
        if (Input.GetKey(KeyCode.K))
        {
            emission.rateOverTime = 500f;
        }
    }
}
