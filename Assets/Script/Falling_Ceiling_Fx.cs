using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Ceiling_Fx : MonoBehaviour
{
    public AudioSource fallingCeiling; 

    // Start is called before the first frame update
    void Start()
    {
        fallingCeiling = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" )
        {
            fallingCeiling.Play();
            Debug.Log("checkpoint");
            //StartCoroutine(fallingCeilingAudioPlay());
        }

    }
    /*
    IEnumerator fallingCeilingAudioPlay()
    {
        fallingCeiling.Play();
        yield return new WaitForSeconds(0.1f);
    }*/
}
