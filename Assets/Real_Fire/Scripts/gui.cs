using UnityEngine;
using System.Collections;

public class gui : MonoBehaviour {
	public Material mat;
	public Material mat1;
	string name="DAY";
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	 void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 150, 100), name)){
           if (name=="DAY"){
			RenderSettings.skybox = new Material(mat);
			name="NIGHT";
				GameObject.Find("sun").GetComponent<Light>().intensity=0.1f;
			}
			else { 
			RenderSettings.skybox = new Material(mat1);
			name="DAY";
				GameObject.Find("sun").GetComponent<Light>().intensity=0f;
			}
        print (mat);
		}
    }
}
