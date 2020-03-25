using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float rotationSpeed = 2;
	public bool rotate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rotate) {
			transform.Rotate (Vector3.forward * rotationSpeed);
		}
	}
}
