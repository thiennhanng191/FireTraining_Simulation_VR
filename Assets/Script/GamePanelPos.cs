using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelPos : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(2,0,0);
        /*
        var xposn = transform.position.x;
        var yposn = transform.position.x;
        var zposn = transform.position.x;

        xposn = player.transform.position.x + 0.5f;
        yposn = player.transform.position.y + 1f;
        zposn = player.transform.position.z + 0.5f;
        */
    }
}
