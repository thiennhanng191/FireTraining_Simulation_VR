using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class HandControllerLeft : MonoBehaviour {

    private Animator animator;

    private void OnEnable()
    {
        // Subscribe to press and release events for drawing
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
    }

    private void OnDisable()
    {
        InteractionManager.InteractionSourcePressed -= InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased -= InteractionSourceReleased;
    }

    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Left && obj.pressType == InteractionSourcePressType.Grasp)
        {
            animator.SetBool("isGrabbing", true);
            //animator.SetBool("isGrabbing");
        }
    }

    private void InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Left && obj.pressType == InteractionSourcePressType.Grasp)
        {
            animator.SetBool("isGrabbing", false);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //animator.SetBool("isGrabbing", Input.GetButton("Grip Press Right"));
        //animator.SetBool("isGrabbing", Input.GetButton("Grip Press Left"));
    }
}
