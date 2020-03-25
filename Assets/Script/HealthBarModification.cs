using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBarModification : MonoBehaviour
{
    public Image healthBar;
    public float decreasingAmount = 0.0005f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ceiling" || collision.gameObject.tag == "ShatteredGlass")
        {
            healthBar.fillAmount -= decreasingAmount; 
        }
    }
}
