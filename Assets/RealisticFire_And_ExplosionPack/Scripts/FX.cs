using UnityEngine;
using System.Collections;

public class FX : MonoBehaviour
{
    public GameObject[] fx = null;
    int num = 0;

	void Start ()
    {
        fx[num].SetActive(true);
        num++;
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 75), "Next"))
        {
            if (num > 0)
            {
                fx[num - 1].SetActive(false);
            }

            if (num == 6)
            {
                num = 0;
            }

            fx[num].SetActive(true);
            num++;
        }
    }
}
