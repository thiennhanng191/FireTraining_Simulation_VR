using UnityEngine;
using System.Collections;

public class flickering : MonoBehaviour {
	private float intensity=0f;
	private float t=0f;
	private float randomizer=0f;
	// Use this for initialization
	void Start () {
	intensity = this.GetComponent<Light>().intensity;
	randomizer = Random.Range(.75f,1.25f);//making each light flickering randomly
	}
	
	// Update is called once per frame
	void Update () {
	t+=Time.deltaTime*randomizer;
		this.GetComponent<Light>().intensity = intensity + Mathf.Sin(t*(1-Mathf.Sin(t*25f))*5f)*intensity/5f;
	}
}
