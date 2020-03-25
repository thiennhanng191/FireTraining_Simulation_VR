using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFireEmitted : MonoBehaviour
{
    public ParticleSystem[] fires;

    public static bool isFireEmitted = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < fires.Length; i++)
        {
            fires[i] = fires[i].GetComponent<ParticleSystem>();
            var fireEmission = fires[i].emission;
            if (fireEmission.enabled)
            {
                isFireEmitted = true; //there are still fires left
            }
        }
    }
}
