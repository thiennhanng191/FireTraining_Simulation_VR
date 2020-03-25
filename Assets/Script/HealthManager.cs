using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthManager : MonoBehaviour
{
    public static float healthData;

    public GameObject healthBox;

    public Image healthBar; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthData = healthBar.fillAmount * 100;

        healthBox.GetComponent<Text>().text = "" + healthData.ToString("F0"); 
    }
}
