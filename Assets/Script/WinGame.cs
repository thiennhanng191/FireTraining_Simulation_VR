using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject gameWinPanelPosition; //the game object attached inside the MixedRealityCameraParents

    public GameObject gameWinPanel;
    

    // Update is called once per frame
    void Update()
    {
        if (!CheckFireEmitted.isFireEmitted)
        {
            gameWinPanel.transform.position = gameWinPanelPosition.transform.position;
            gameWinPanel.SetActive(true);
        }
    }
}
