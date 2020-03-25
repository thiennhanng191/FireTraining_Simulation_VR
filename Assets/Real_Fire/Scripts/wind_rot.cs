using UnityEngine;
using System.Collections;

public class wind_rot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	this.transform.Rotate(Vector3.up,.05f);
	}
}
