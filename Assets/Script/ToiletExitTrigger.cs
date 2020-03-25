using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletExitTrigger : MonoBehaviour
{
    public GameObject previousInstructionPanel;
    public GameObject nextInstructionPanel;

    void OnTriggerEnter(Collider other)
    {
        if (previousInstructionPanel.activeSelf == true)
        {
            if (other.tag == "Player")
            {
                nextInstructionPanel.SetActive(true);
                previousInstructionPanel.SetActive(false);
            }
        }
    }
}
