using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanelPosition; //the game object attached inside the MixedRealityCameraParents

    public Image healthBar;

    public GameObject gameOverPanel; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.fillAmount <= 0f)
        {
            //FreezeGame.Pause();
            gameOverPanel.transform.position = gameOverPanelPosition.transform.position;
            gameOverPanel.SetActive(true);
        }
    }
}
