using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject endGameCanvas;
    public GameObject timeManager;
    public GameObject healthManager; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endGameCanvas.SetActive(true);
            timeManager.SetActive(false);
            healthManager.SetActive(false);
        }
    }
}
