using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBarControl : MonoBehaviour
{
    public Image healthBar;
    public int delayAmount = 1; 

    protected float timer;
    private float decreasingAmount = 0.0005f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //decreasingAmount += 0.005f;
        
        if (timer >= delayAmount)
        {
            timer = 0f;
            healthBar.fillAmount -= decreasingAmount;
        }

    }
    /*
     void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire")
        {
            if (other.GetComponent<ParticleSystem>().emission.enabled == true)
            {
                healthBar.fillAmount -= 0.005f;
                Debug.Log(healthBar.fillAmount.ToString());
            }
        }

    } 
    */
}
