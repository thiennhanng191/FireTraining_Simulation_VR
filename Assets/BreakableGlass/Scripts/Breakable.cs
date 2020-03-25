using UnityEngine;
using System.Collections;


public class Breakable : MonoBehaviour {

	/*
	Thank you for purchasing this breakable glass asset by Patrick Ball! Please email any suggestions or comments to mr.patrick.ball@gmail.com. Feedback on the Unity Asset Store is always appreciated. Unless it's bad - in which case it's appreciated significantly less.
***************************************
	Simply call the "BreakFunction ()" function on anything with this script attached to break the object
***************************************
	-> Object must have at least one replacement with which to replace itself upon breaking. Assign the replacement gameobject in the first public assignment slot in the inspector.

	*/

//------------------------------------------------------------------------------------
//--These are the Public Variables that you can toy around with in the inspector. I gave them long names so it would be easier to decifer their purpose. 
//---------------------------------------------------------------------------------
	//Set these in the inspector. All Breakable items must have at least 1 replacement here to function properly.
	public GameObject[] Replacements;
	
	//If this option is checked, any collision will call the break function on this object.
	public bool BreakOnCollision = true;
	//This float combines the velocity of the rigidbody of this object(if one is attached) with the velocity of the rigidbody of the object this is hitting (if one is attached)
	//and only breaks the object if the objects' combined velocity is above this value. set value to 0 to ensure it will ALWAYS break on contact - however slow or light.
	public float SpeedRequiredToBreak = 0f;

	//Cleans up shards, giving them a random lifetime between the two values.
	public bool CleanUpShards = true;
	public float MinShardLife = 3f;
	public float MaxShardLife = 6f;

	//Restores the Breakable Object to its original condition - before being broken.
	public bool Repair = true;
	//ReparTime should always be GREATER THAN MaxShardLife to avoid instantiating too many replacements at once.
	public int RepairTime = 6;

	//These options are for when you want the shards to be of a different material than the original - like a lightbulb being shot out, for instance.
	public bool replaceMaterialOnBreak = false;
	public Material ReplacementMaterial;
	
	//This makes it so that the shards' mass and drag scale appropriately with the objects size (transform.localscale)
	//Can change to false without much difference. To anything. :-P
	public bool ShardMassChangesWithScale = true;

	//Checking this option makes the object call its replacement, but then immediately tell all the rigidbodies in the replacement to be knematic. In this way, the object 'cracks' but does not shatter
	//...unless you hit it again - then it shatters.
	public bool CrackBeforeShattering;

	public bool PlaySound = true;

	public AudioClip[] ShatterSoundClips;
	public AudioClip[] CrackSoundClips;




//----------------------------------------------------------------------------------
//The following variables are not accessable outside this Class. It is less obvious what these do.
//---------------------------------------------------------------------------------------

	//Has the object already been crackd, and is awaiting either automaic cleanup or shattering? 
	[HideInInspector]
	public bool Cracked = false;
	//prevents instantiating too many replacements on Break().
	private bool Broken = false;

	//holds the attached AudioSource Component.
	private AudioSource AttachedAudioSource;

	//holds the material of the original window, so that the shattered replacement can be the same material.
	private Material OriginalMat;
	//holds the material of the original lid/cork/accessory if one is present
	private Material AccessoryMat;
	
	//a place to store the renderers of all the incoming shards, to make the materials match.
	private Renderer[] ShardsMats;

	//A place to store the rigidbodies of the shards so you can...uh... do stuff to them.
	private Rigidbody[] ShardsRigidbodies;

	//This is a placeholder for the rigidbody, if one is attached to this object.
	private Rigidbody ThisRigidbody;
	//this bool is here to automatically handle behavior based on weather the attached rigidbody is kinematic or not.
	private bool IsRigidbodyKinematic = false;
	//these floats hold the velocity.magnitude of the rigidbody of this object and whatever object this object collides with.
	
	//these two variables are combined to create a threshold that must be reached before breaking can occur, if set to BreakOnCollision.
	private float SpeedOfOther = 0;
	private float SpeedOfSelf = 0;
	
	//a place to store the original position and rotation of the object if it has a rigidbody, and may have moved. 
	//this makes it so that when this object repairs, it goes to it's original position.
	private Vector3 StartPosition;
	private Vector3 StartRotation;

	//To select which replacement to use at random, so it doesn't look the same every time it gets broken.
	private int RandomReplacementNumber;

	//Used to select which of the available sounds to play.
	private int SoundNumber;

	//used internally, and composed of MinShardLife and MaxShardLife.
	private float ShardLifeTime;


//----------------------------------------------------------------------------------------------------


	void Start () {


		// Determines if there is an audiosource attached, then assigns the private variable. 
		//If there is no AudioSource Compnent Attached, 'PlaySound' is declared false to avoid errors occuring 
		//because of trying to access an audiosource that isn't present.
		if (GetComponent<AudioSource> () != null) {
			AttachedAudioSource = GetComponent<AudioSource> ();
		} 
		else {
			PlaySound = false;
		}

		// This finds if there is a rigidbody, stores the rigidbody in a variable, 
		//then notes weather it is kinematic to determine repair behavior later in the script
		if (GetComponent<Rigidbody>() != null){
			ThisRigidbody = GetComponent<Rigidbody>();
			if (ThisRigidbody.isKinematic == true){
				IsRigidbodyKinematic = true;
			}
			else{
				IsRigidbodyKinematic = false;
			}
			if(Repair){
				StartPosition = transform.position;
				StartRotation = transform.rotation.eulerAngles;
			}
		}

		//This bit of code stores the material of the lid/cork/accessory if one is available. Must be FIRST child of this breakable object, and must have tag "GlassAccessory"
		if (transform.childCount > 0) {
			if (transform.GetChild (0).gameObject.GetComponent <Renderer> () != null) {
				AccessoryMat = transform.GetChild (0).gameObject.GetComponent <Renderer> ().material;
			}
		}
	}



//------------------------------------------------------------------------------
	// If the public variable "BreakOnCollision" (above) is set to 'true', then when this object comes 
	// into contact with a gameobject which has a collider attached, then the 'Break()' function will be called on this object. Behold!
    /*
	void OnCollisionEnter (Collision other){

		if (BreakOnCollision) {
		
			if (other.rigidbody != null) {
				SpeedOfOther = other.rigidbody.velocity.magnitude;
			}
			if (ThisRigidbody != null) {	
				SpeedOfSelf = ThisRigidbody.velocity.magnitude;
			}
			float CombinedSpeed = SpeedOfOther + SpeedOfSelf;

			if (CombinedSpeed >= SpeedRequiredToBreak) {

				BreakFunction ();
			}
		}
	}
    */

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BreakFunction(); 
        }
    }
    */


    //------------------------------------------------------------------------------------------------------------------------------
    //SIMPLY CALL THIS FUNCTION TO BREAK THE GLASS     !!!!!!!!!!!!!!!!!!!!!!!!!!!
    //****************************************************
    //----------------------------------------------------------------------------------------------------------------------------------------
    public	void BreakFunction (){
		if (!Cracked){
			if (!Broken){
			StartCoroutine (Break());
			}
		}
		else if (Cracked) {
			ShatterAfterCrackingFunction(ShardsRigidbodies);
		}
	}




/*-----------------------------------------------------------------
The following 'Break' coroutine contains most of the logic for this script
	Breakdown:
	1. Checks to see if replacement(s) has any gamobjects assigned in the inspector.
	2. Assigns a number to select a random replacement, then instantiates the
		new replacement in the same place, scale and rotation.
	3. Gathers materials of shards into an array, then assigns the material of the shards according to the 'ReplaceMaterialOnBreak' boolean.
		assigns the material of the lid/cork/accessory if one is present(FIRST child).
	4. Gathers the rigidbodies of the new shards into an array, then changes
		the mass and drag of each rigidbody based on the original object's transform.localscale.
	5. cleans up shards by accessing the newly-stored rigidbodies.
	6. Uses Rigidbodies again to do the "crack before breaking" function (if enabled)
	7. disables original model's collider and renderer, then plays appropriate random sound
	8. re-enables original models's collider and renderer (if "repair" is set to true - otherwise destroys this object).
	
------------------------------------------------------------------
	 */
	IEnumerator Break (){

//1.
		if (Replacements.Length > 0) {
//2.		This will be a number between 1 and the number of possible (assigned) replacements.
			RandomReplacementNumber = Random.Range (0, Replacements.Length);

			GameObject NewReplacement = Instantiate (Replacements [RandomReplacementNumber], transform.position, transform.rotation) as GameObject;

			Broken = true;

			NewReplacement.transform.localScale = this.transform.localScale;

//3.		Stores all the shard's materials in an array. Orignally just for automatically setting the material of the shards based on the material of the origina
			//could use these mats to do all kinds of stuff I couldn't possibly predict. 
			ShardsMats = NewReplacement.GetComponentsInChildren<Renderer> ();

	//This is the part that sets the materials of the Shards
			if (!replaceMaterialOnBreak) {
				if (GetComponent <Renderer> () != null) {
					OriginalMat = GetComponent<Renderer>().material;
				
				for (int i = 0; i < ShardsMats.Length; i++) {
					if (ShardsMats[i].transform.tag != "GlassAccessory"){
						ShardsMats [i].GetComponent<Renderer> ().material = OriginalMat;
						}
				}
				}
			} else {
				for (int i = 0; i < ShardsMats.Length; i++) {
					ShardsMats [i].GetComponent<Renderer> ().material = ReplacementMaterial;
				}

			}
	//This is the part that assigns the material to the lid/cork/accessory, if one is present
			if (NewReplacement.transform.childCount > 0){
				if (NewReplacement.transform.GetChild(0).tag == "GlassAccessory"){
					if (NewReplacement.transform.GetChild (0).gameObject.GetComponent <Renderer>() != null){
					NewReplacement.transform.GetChild(0).gameObject.GetComponent <Renderer>().material = AccessoryMat;
				}

			}
			}

//PLACE CODE TO DO CUSTOM STUFF TO THE SHARDS' Materials BELOW THIS LINE
			
			
//PLACE CODE TO DO CUSTOM STUFF TO THE SHARDS' Materials ABOVE THIS LINE
//this space will be left in on updates.


//4.		//stores all the shards' rigidbodies in an array for... doing stuff to. like making kinematic or applying force... 
			ShardsRigidbodies = NewReplacement.GetComponentsInChildren<Rigidbody> ();

			//... or like adjusting the mass and drag based on scale.
			if (ShardMassChangesWithScale){
			
			float xSize =transform.localScale.x;
			float ySize =transform.localScale.y;
			float zSize =transform.localScale.z;
			float NewMass = ((xSize + ySize + zSize) / 3)/2;
			float NewDrag = Mathf.Clamp(NewMass, 0.1f, 4f);
				for (int i = 0; i < ShardsRigidbodies.Length; i++){
					ShardsRigidbodies[i].mass = NewMass;
					ShardsRigidbodies[i].drag = NewDrag;

				}

			
			}

//PLACE CODE TO DO CUSTOM STUFF TO THE SHARDS' RIGIDBODIES BELOW THIS LINE


//PLACE CODE TO DO CUSTOM STUFF TO THE SHARDS' RIGIDBODIES ABOVE THIS LINE
//this space will be left in on updates.

//5.		Cleans up shards using rigidbodies stored in step 4.
			if (CleanUpShards) {

				for (int i = 0; i < ShardsRigidbodies.Length; i++) {
					if (ShardsRigidbodies[i].gameObject != null){
					ShardLifeTime = Random.Range (MinShardLife, MaxShardLife);
					Destroy (ShardsRigidbodies [i].gameObject, ShardLifeTime);
					}
				}
				Destroy (NewReplacement, MaxShardLife);
			}

//6.		//If CrackBeforeShattering is true, then this will hand control over to the CrackFunction.
			//cracks glass but doesn't shatter by making pieces kinematic.
			//calling the BreakFunction() again will shatter it.
			if (CrackBeforeShattering) {
				CrackFunction (ShardsRigidbodies);
				Cracked = true;
			}
	
		
//7.		//disables the original model, so you don't have a broken model and a whole model at the same time.
			GetComponent<Renderer> ().enabled = false;
		
			if (!CrackBeforeShattering) {
				PlayShatterSound ();
				GetComponent<Collider> ().enabled = false;
			}
			//disables the FIRST child ONLY, --this is used to handle things like the teapot lid, the corks, or an attached light... Or whatever.
			if (transform.childCount > 0){
				transform.GetChild(0).gameObject.SetActive(false);
			}


//8.		//IF THE 'Repair' BOOLEAN IS SET TO FALSE, THE OBJECT THAT THIS SCRIPT IS ON GETS DESTROYED.
			if (Repair) {
				yield return new WaitForSeconds (RepairTime);

				if (ThisRigidbody != null) {
					ThisRigidbody.velocity = Vector3.zero;
					ThisRigidbody.isKinematic = true;
					transform.position = StartPosition;
					transform.rotation = Quaternion.Euler(StartRotation);
				}
				RepairFunction (NewReplacement);

			} 
			if (Repair == false) {
				if (Cracked){
				Destroy (this.gameObject, MaxShardLife + 3 );
				}
			}
		}
	}

	//The End!
//------------------------------------------------------
//The following functions are called in other functions. they are separated out for the sake of ecapsulation and extendability.
//-----------------------------------------------------


	//Call this function to instantly repair this object. Used in the Break() coroutine above.
	//if object has a non-kinematic rigidbody, the objects position, rotation, and velocity will be returned to their original values.
public	void RepairFunction (GameObject BrokenVersion){
		Destroy (BrokenVersion);
		GetComponent<Renderer> ().enabled = true;
		GetComponent<Collider> ().enabled = true;
		Cracked = false;
		Broken = false;
		if (ThisRigidbody != null) {
			if (!IsRigidbodyKinematic){
			ThisRigidbody.isKinematic = false;
			}
		}
	//this re-enables the accessory (lid, cork, light). only works for FIRST child.
		if (transform.childCount > 0){
			transform.GetChild(0).gameObject.SetActive(true);
		}

	}



		//This function disables the physics of the new glass shards, so they don't fly away or drop.
		//it also mlays a random shatter sound if one is attached.
	void CrackFunction (Rigidbody[] Shards){
		Cracked = true;
		PlayCrackingSound ();

		for (int i = 0; i < Shards.Length; i++) {
			Shards [i].isKinematic = true;
			Shards [i].GetComponent<Collider>().enabled = false;
		}
	}


	//...And THIS function re-enables the physics of the glass shards, so they DO fly away and/or drop. 
	//it also mlays a random shatter sound if one is attached.
	void ShatterAfterCrackingFunction (Rigidbody[] Shards){
		GetComponent<Collider> ().enabled = false;
		
		PlayShatterSound ();

		for (int i = 0; i < Shards.Length; i++) {
			if(Shards[i] != null){
			Shards [i].isKinematic = false;
			Shards [i].GetComponent <Collider>().enabled = true;
		}
		}
		if (!Repair){
		Destroy (this.gameObject, MaxShardLife + 3 );
		}

	}



	void PlayShatterSound (){
		if (PlaySound){
			if (ShatterSoundClips.Length > 0){
				SoundNumber = Random.Range (0, ShatterSoundClips.Length);
				AttachedAudioSource.clip = ShatterSoundClips[SoundNumber];
				AttachedAudioSource.Play ();
			}
		}
	}

	void PlayCrackingSound (){
		if (PlaySound){
			if (CrackSoundClips.Length > 0){
				SoundNumber = Random.Range (0, CrackSoundClips.Length);
				AttachedAudioSource.clip = CrackSoundClips[SoundNumber];
				AttachedAudioSource.Play ();
			}
		}
	}

}
