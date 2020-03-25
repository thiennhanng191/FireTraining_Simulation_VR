using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Controller : MonoBehaviour
{
    public ParticleSystem fireHose;

    void Start()
    {
        InteractionManager.InteractionSourcePressed += InteractionHandling;
        fireHose = fireHose.GetComponent<ParticleSystem>();
    }

    void InteractionHandling(InteractionSourcePressedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Left && obj.pressType == InteractionSourcePressType.Select)
        {
            Debug.Log("plz work");
            var emission = fireHose.emission;
            //emission.rateOverTime = 0f;
            if (CheckGrabFireExtinguisher.checkHandle)
            {
                Debug.Log("hello world");
                emission.rateOverTime = 200f;
                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
