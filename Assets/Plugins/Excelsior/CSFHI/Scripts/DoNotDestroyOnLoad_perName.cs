using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoNotDestroyOnLoad_perName : MonoBehaviour {
    public static List<string> list = new List<string>();
	// Use this for initialization
    void Awake() {
        if (list.Contains(this.name)) {
            GameObject.Destroy(this.gameObject);
        }
    }
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        list.Add(this.name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
