using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanelControl : MonoBehaviour
{
    public ParticleSystem checkpointFire;
    public GameObject previousInstructionPanel;
    public GameObject nextInstructionPanel; 


    // Start is called before the first frame update
    void Start()
    {
        checkpointFire = checkpointFire.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        var checkPointFireEmission = checkpointFire.emission;

        if (checkPointFireEmission.enabled)
        {
            nextInstructionPanel.SetActive(true);
            previousInstructionPanel.SetActive(false);
        }

    }
}

