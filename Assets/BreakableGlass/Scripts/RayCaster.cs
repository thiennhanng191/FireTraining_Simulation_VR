using UnityEngine;
using System.Collections;

/*
	Thank you for purchasing this breakable glass asset by Patrick Ball! Please email any suggestions or comments to mr.patrick.ball@gmail.com. Feedback on the Unity Asset Store is always appreciated. Unless it's bad - in which case it's appreciated significantly less.
***************************************
This is a standard Ray-Casting script with some extra options for adding explosions ... MMMMmmmmm, explosions.
	*/

public class RayCaster : MonoBehaviour {

	//this is a place to assign the camera to shoot the ray. if left blank, and this class is placed onto a camera in the scene, the bit of code in the Start() method will automatically assign it.
	// if it's left blank, and placed on something other than a camera... then it won't work.
	public Camera thisCam;
	
	// some code to add explosive force to rigidbodies laying around nearby. always fun.
	public bool addExplosiveForceToNearbyRigidbodies;
	public float explosiveRadius = 3;
	public float explosiveForce = 500;
	public float explosiveUpwardsModifier = 50;

	//if this option is checked, all objects with the Breakable class attached that are within range (DistanceToBreak) will have the BreakFunction() function called on them.
	public bool BreakNearbyBreakables;
	public float DistanceToBreak = 3;

	//if this option is ticked, then the breakables being broken won't crack before breaking, even if they are individually set to do so.
	public bool IgnoreCrackFunction;

	//added bool to enable continuous fire (like machineguns) so the ray is cast continuously as long as the mouse is held down. (Thanks to Defiant Child for this suggestion!)
	public bool ContinuousFire;
	public float RateOfFire = 0.1f;
	private bool RateOfFireSwitch;

	//If you have your own explosion particlesystem, you can assign it here and check this option so that kablooweys will show up wherever you click.
	public bool InstantiateParticleSystem = false;
	public GameObject particleSystemNotIncluded;


	// This line of code automatically assigns thisCam as the attached camera component if one is not already assigned.
	void Start () {

		if (thisCam == null) {
			if (GetComponent<Camera>() != null){
				thisCam = GetComponent<Camera>();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (!ContinuousFire) {
			if (Input.GetMouseButtonDown (0)) {
				
				//Your standard ray cast. creates a ray, then stores information about what the ray hit in the 'hit' variable. 
				//then casts the ray, and calls the BreakFunction() on whatever collider it hits... 
				//provided that the collider is attached to a gameobject that also has the Breakable class attached.
				Ray ray = thisCam.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {
					if (hit.transform.GetComponent<Breakable> () != null) {
						Breakable ThisBreakable = hit.transform.GetComponent<Breakable> (); 
						if (IgnoreCrackFunction) {
							StartCoroutine (TemporarilyDisableCracking (ThisBreakable));
						}
						ThisBreakable.SendMessage ("BreakFunction");
					}
					
					//these pass control to the functions below if the appropriate bools are set to true.
					if (BreakNearbyBreakables) {
						BreakBreakables (hit.point);
					}
					
					if (addExplosiveForceToNearbyRigidbodies) {
						StartCoroutine (AddBoom (hit.point));
					}
					
					if (InstantiateParticleSystem) {
						AddBoomParticles (hit.point);
					}
					
				}
			}
		}

		//clicking the mouse casts a ray at what you were pointing at on screen - then does stuff to the first collider it finds in the path of that ray.
		else if (ContinuousFire) {
			if (Input.GetMouseButton (0)) {

				StartCoroutine (DelayBetweenCrackAndFire ());


			}
		}
	}

//this function adds explosive force to rigidbodies laying around within range (explosiveRadius). Like the shards from the glass, for instance.
public	IEnumerator AddBoom (Vector3 HitSpot){

		// this line gives an extra frame for the shattered replacement object to instantiate.
		yield return null;

		//this stores the colliders within range into an array, then adds the explosive force to them.
		Collider[] shards = Physics.OverlapSphere (HitSpot, explosiveRadius);
		for (int i = 0; i < shards.Length; i++) {
			if (shards [i].GetComponent<Rigidbody> () != null) {
				if (shards [i].GetComponent<Rigidbody> ().isKinematic == false) {
					shards [i].GetComponent<Rigidbody> ().AddExplosionForce (explosiveForce, HitSpot, explosiveRadius);
				}
			}
		}
	}

public void BreakBreakables (Vector3 HitSpot){
		Collider[] Breakables = Physics.OverlapSphere (HitSpot, DistanceToBreak);
		for (int i = 0; i < Breakables.Length; i++) {
			if (Breakables [i].GetComponent<Breakable> () != null) {
				Breakable ThisBreakable = Breakables[i].GetComponent<Breakable>(); 
				if (IgnoreCrackFunction){
					StartCoroutine (TemporarilyDisableCracking(ThisBreakable));
				}
				ThisBreakable.BreakFunction ();
			}
		}
	}

	//This will only work if you have your own explosion particle system - not included.
	public void AddBoomParticles (Vector3 HitSpotParticles){
		//this part puts an explosion particle effect wherever you clicked... particle system Not included.
		if (particleSystemNotIncluded != null) {
			Instantiate (particleSystemNotIncluded, HitSpotParticles, Quaternion.identity);
		}
	}

	public IEnumerator TemporarilyDisableCracking(Breakable BreakableInQuestion){
		bool OriginalCondition = BreakableInQuestion.CrackBeforeShattering;
		if (OriginalCondition == true) {
			BreakableInQuestion.CrackBeforeShattering = false;
			yield return new WaitForSeconds (1);
			BreakableInQuestion.CrackBeforeShattering = true;
		}

	}

	public IEnumerator DelayBetweenCrackAndFire (){
	
		if (!RateOfFireSwitch){

			Ray ray = thisCam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (BreakNearbyBreakables) {
					BreakBreakables (hit.point);
				}
				
				if (addExplosiveForceToNearbyRigidbodies) {
					StartCoroutine (AddBoom (hit.point));
				}
				
				if (InstantiateParticleSystem) {
					AddBoomParticles (hit.point);
				}
				if (hit.transform.GetComponent<Breakable> () != null) {
					Breakable ThisBreakable = hit.transform.GetComponent<Breakable> (); 
					if (IgnoreCrackFunction) {
						StartCoroutine (TemporarilyDisableCracking (ThisBreakable));
					}

					ThisBreakable.SendMessage ("BreakFunction");
					RateOfFireSwitch = true;
					yield return new WaitForSeconds (RateOfFire);
					RateOfFireSwitch = false;
				}
				

				
			}


		}
		
	}




}
