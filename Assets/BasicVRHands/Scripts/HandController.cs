using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA.Input;

public class HandController : MonoBehaviour {

    private Animator animator;
    public ParticleSystem fireHose;
    public ParticleSystem fireHoseComponent1;
    public ParticleSystem fireHoseComponent2;
    public GameObject virtualBox;


    private void OnEnable()
    {
        // Subscribe to press and release events for drawing
        
    }

    /*private void OnDisable()
    {
        InteractionManager.InteractionSourcePressed -= InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased -= InteractionSourceReleased;
    }*/

    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Left && obj.pressType == InteractionSourcePressType.Grasp)
        {
            Debug.Log("àdghfhfgd");
            animator.SetBool("isGrabbing", true);
            var fireEmission = fireHose.emission;
            var fireComponent1Emission = fireHoseComponent1.emission;
            var fireComponent2Emission = fireHoseComponent2.emission;

            //emission.rateOverTime = 0f;
            if (CheckGrabFireExtinguisher.checkHandle)
            {
                fireEmission.enabled = true;
                fireComponent1Emission.enabled = true;
                fireComponent2Emission.enabled = true;
                virtualBox.SetActive(true);
            }
            //animator.SetBool("isGrabbing");
        }
    }

    private void InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Left && obj.pressType == InteractionSourcePressType.Grasp)
        {
            animator.SetBool("isGrabbing", false);
            var fireEmission = fireHose.emission;
            var fireComponent1Emission = fireHoseComponent1.emission;
            var fireComponent2Emission = fireHoseComponent2.emission;

            //emission.rateOverTime = 0f;
            if (!CheckGrabFireExtinguisher.checkHandle)
            {
                fireEmission.enabled = false;
                fireComponent1Emission.enabled = false;
                fireComponent2Emission.enabled = false;
                virtualBox.SetActive(false);
            }
        }
    }

    void Awake () {
        animator = GetComponent<Animator>();
        fireHose = fireHose.GetComponent<ParticleSystem>();
        fireHoseComponent1 = fireHoseComponent1.GetComponent<ParticleSystem>();
        fireHoseComponent2 = fireHoseComponent2.GetComponent<ParticleSystem>();
    }
    void Start()
    {
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
    }
    void Update () {
        //animator.SetBool("isGrabbing", Input.GetButton("Grip Press Right"));
        //animator.SetBool("isGrabbing", Input.GetButton("Grip Press Left"));
    }
}
