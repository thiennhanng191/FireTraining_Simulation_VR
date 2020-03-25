using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
I spaghettied this together to make the demo scene. 
*/


public class DemoSceneClass : MonoBehaviour {

	// an array of waypoints for the camera to slide between.
	public Transform[] wayPoints;

	//for lerping between different waypoints in front of setups (displays)
	public float slidespeed = .2f;

	//used in GoBack() and GoForward() functions below.
	private int displaySelection;

	public RayCaster CamCaster;

	// Use this for initialization
	void Start () {
		displaySelection = 0;
		CamCaster = GetComponent <RayCaster>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, wayPoints[displaySelection].position, slidespeed);
	}

	public void GoForward (){
		int maxWaypoints = wayPoints.Length - 1;
		if (displaySelection < maxWaypoints ){
			displaySelection ++;
		}
	}

	public void GoBack (){
		if (displaySelection > 0 ){
			displaySelection --;
		}
	}

	public void ToggleAddExplosion (){
		if (CamCaster.addExplosiveForceToNearbyRigidbodies){
			CamCaster.addExplosiveForceToNearbyRigidbodies = false;
		}
		else if (!CamCaster.addExplosiveForceToNearbyRigidbodies){
			CamCaster.addExplosiveForceToNearbyRigidbodies = true;
		}

		if (CamCaster.BreakNearbyBreakables){
			CamCaster.BreakNearbyBreakables = false;
		}
		else if (!CamCaster.BreakNearbyBreakables){
			CamCaster.BreakNearbyBreakables = true;
		}

		if (CamCaster.IgnoreCrackFunction){
			CamCaster.IgnoreCrackFunction = false;
		}
		else if (!CamCaster.IgnoreCrackFunction){
			CamCaster.IgnoreCrackFunction = true;
		}
	}
}
