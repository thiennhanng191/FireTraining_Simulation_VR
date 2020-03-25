using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCeiling : MonoBehaviour
{
    public GameObject[] fakeCeilingTiles;
    public GameObject[] fakeCeilingHoles;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(fallingCeilingTiles());
        }
    }

    IEnumerator fallingCeilingTiles()
    {
        for (int i = 0, j = 0; i < fakeCeilingTiles.Length && j < fakeCeilingHoles.Length; i++, j++)
        {
            fakeCeilingTiles[i].SetActive(true);
            fakeCeilingHoles[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
