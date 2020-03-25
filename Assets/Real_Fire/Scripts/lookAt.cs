using UnityEngine;
using System.Collections;

public class lookAt : MonoBehaviour {
	public Transform player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	this.transform.LookAt(player);
	}
}
