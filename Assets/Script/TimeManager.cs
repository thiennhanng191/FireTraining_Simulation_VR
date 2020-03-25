using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TimeManager : MonoBehaviour
{
    public static int minCount;
    public static int secCount;
    public static float milCount;
    public static string milDisplay;

    public static float realTime;

    public GameObject minBox;
    public GameObject secBox;
    public GameObject milBox;

    void Update()
    {
        milCount += Time.deltaTime * 10;
        realTime += Time.deltaTime;
        milDisplay = milCount.ToString("F0"); //F0 to display the value with 0 decimal places
        milBox.GetComponent<Text>().text = "" + milDisplay;

        if (milCount >= 10)
        {
            milCount = 0;
            secCount += 1;
        }

        if (secCount <= 9)
        {
            secBox.GetComponent<Text>().text = "0" + secCount + ".";
        }
        else
        {
            secBox.GetComponent<Text>().text = "" + secCount + ".";
        }

        if (secCount >= 60)
        {
            secCount = 0;
            minCount += 1;
        }

        if (minCount <= 9)
        {
            minBox.GetComponent<Text>().text = "0" + minCount + ".";
        }
        else
        {
            minBox.GetComponent<Text>().text = "" + minCount + ".";
        }
    }

}
