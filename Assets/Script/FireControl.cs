using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    public GameObject[] fires;

    //public ParticleSystem[] fireComponents;
    /*
    public ParticleSystem hose; 
    public ParticleSystem outsideFire;
    public ParticleSystem centerFire;
    public ParticleSystem sparks;
    public ParticleSystem smoke;
    public Light lightFire;
    */
     private float emissionRate = 200f; 

    List<ParticleCollisionEvent> collisionEvents;  
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        hose = hose.GetComponent<ParticleSystem>();
        outsideFire = outsideFire.GetComponent<ParticleSystem>();
        centerFire = centerFire.GetComponent<ParticleSystem>();
        sparks = sparks.GetComponent<ParticleSystem>();
        smoke = smoke.GetComponent<ParticleSystem>();
        lightFire = lightFire.GetComponent<Light>();
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Hose")
        {
            var fireEmission = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
            emissionRate -= 10f;
            fireEmission.rateOverTime = emissionRate;
            if (emissionRate <= 10)
            {
                fireEmission.enabled = false;
                for (int i = 0; i < transform.GetChild(0).transform.childCount; i++)
                {
                    Debug.Log("Plz work22222");
                    var fireComponentsEmission = transform.GetChild(0).transform.GetChild(i).GetComponent<ParticleSystem>().emission;
                    fireComponentsEmission.enabled = false;
                }
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
   
        if (other.tag == "Fire")
        {
            var fireEmission = other.transform.GetChild(0).GetComponent<ParticleSystem>().emission;

            Debug.Log("Checkpoint10"); 
            //ParticlePhysicsExtensions.GetCollisionEvents(hose, other, collisionEvents);

            //int numCollisionEvents = hose.GetCollisionEvents(other, collisionEvents);

            emissionRate -= 10f;

            fireEmission.rateOverTime = emissionRate;

            

            if (emissionRate <= 10) {
                fireEmission.enabled = false;
                for (int i = 0; i < other.transform.GetChild(0).transform.childCount; i++)
                {
                    Debug.Log("Plz work2");
                    var fireComponentsEmission = other.transform.GetChild(0).transform.GetChild(i).GetComponent<ParticleSystem>().emission;
                    fireComponentsEmission.enabled = false;
                }
            }

    



        }
    }
    /*
    void fireControl(ParticleCollisionEvent particleCollisionEvent)
    {
        var outsideFireEmission = outsideFire.emission;
        var centerFireEmission = centerFire.emission;
        var sparksEmission = sparks.emission;
        var smokeEmission = smoke.emission;

        outsideFireEmission.rateOverTime = 0f;
        centerFireEmission.rateOverTime = 0f;
        sparksEmission.rateOverTime = 0f;
        smokeEmission.rateOverTime = 0f;

        Debug.Log("Checkpoint 1"); 
    }
    */
}
