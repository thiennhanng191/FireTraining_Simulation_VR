using UnityEngine;
using System.Collections;

public class SimpleDoorTrigger : MonoBehaviour {
	public Transform Door;
	public float OpenAngleAmount = 88.0f;
	public float SmoothRotation;	
	public string interactText = "Press F To Interact";
	public GUIStyle InteractTextStyle;
		
	private bool init = false;
	private bool hasEntered = false;
	private bool doorOpen = false;
	private Vector3 startAngle;
	private Vector3 openAngle;	
	private Rect interactTextRect;
		
	void Start () {
		//Check if Door Game Object is properly assigned
		if(Door == null){
			Debug.LogError (this + " :: Door Object Not Defined!");
		}
		
		//Init Start and Open door angles
		startAngle = Door.eulerAngles;
		openAngle = new Vector3(startAngle.x, startAngle.y + OpenAngleAmount, startAngle.z);
		
		//Init Interact text Rect
		Vector2 textSize = InteractTextStyle.CalcSize(new GUIContent(interactText));
		interactTextRect = new Rect(Screen.width / 2 - textSize.x / 2, Screen.height - (textSize.y + 5), textSize.x, textSize.y);
		
		init = true;
	}
		
	void Update () {
		if(!init)
			return;
		
		HandleDoorRotation();
		HandleUserInput();	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			hasEntered = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		hasEntered = false;
	}
	
	void OnGUI(){
		if(!init || !hasEntered)
			return;
		
		GUI.Label(interactTextRect, interactText, InteractTextStyle);
	}
	
	void HandleDoorRotation(){        
        if (!doorOpen) {
            Door.rotation = Quaternion.Slerp(Door.rotation, Quaternion.Euler(startAngle), Time.deltaTime * SmoothRotation);            
        }
        else {            
            Door.rotation = Quaternion.Slerp(Door.rotation, Quaternion.Euler(openAngle), Time.deltaTime * SmoothRotation);
        }
	}
	
	void HandleUserInput(){
		if(Input.GetKeyDown(KeyCode.F) && hasEntered){
			doorOpen = !doorOpen;
		}			
	}
}