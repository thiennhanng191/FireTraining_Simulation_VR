using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrabFireExtinguisher : MonoBehaviour
{
    public static bool checkHandle = false;

     void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Hand")
        {
            Debug.Log("plz work plz");
            checkHandle = true;
        }   
    }
}
