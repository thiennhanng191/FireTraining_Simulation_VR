using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour
{
    public GameObject nextInstructionPanel;
    public GameObject previousInstructionPanel; 

     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nextInstructionPanel.SetActive(true);
            previousInstructionPanel.SetActive(false);
        }
    }
}
