using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGame : MonoBehaviour
{
    public static void Pause()
    {
        Time.timeScale = 0;    
    }
}
