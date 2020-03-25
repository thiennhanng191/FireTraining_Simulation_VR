using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]
#endif

//EXCELSIOR SCRIPT
//The InterfaceAnimManager will create an InterfaceAnmElement for each child it contains

[System.Serializable]
public enum CSFHIAnimableState {
	appearing,
	appeared,
	disappearing,
	disappeared
}
[System.Serializable]
public class InterfaceAnmElement : MonoBehaviour {
    public GameObject gameObjectRef;
    public float timeAppear = 1;
    public float timeDisappear = 1;
    public bool recycling = false;
    public int sortID = 0;
    public Animator animator;
    public AnimationClip[] animClips;
    public static List<InterfaceAnmElement> list = new List<InterfaceAnmElement>();
	/*public SerializedProperty serializedPropertyTimeAppear;
	public SerializedProperty serializedPropertyTimeDisappear;
	public SerializedObject serializedObject;*/

    public CSFHIAnimableState currentState = CSFHIAnimableState.disappeared;
    public bool isNested_IAM = false;
    //pooling system
    public static InterfaceAnmElement Create(GameObject _gameObjectRef, int _newSortID, bool _isNested_IAM) {
        InterfaceAnmElement _newElement;
        if (_gameObjectRef.GetComponent<InterfaceAnmElement>()) {
            _newElement = _gameObjectRef.GetComponent<InterfaceAnmElement>();
            return _newElement.Confirm(_gameObjectRef, _newSortID, _isNested_IAM);
        }
        foreach (InterfaceAnmElement _element in list) {
            if (_element.recycling) {
                _newElement = _element;
                return _newElement.Confirm(_gameObjectRef, _newSortID, _isNested_IAM);
            }
        }
        //_newElement = ScriptableObject.CreateInstance<InterfaceAnmElement>();
        _newElement = _gameObjectRef.AddComponent<InterfaceAnmElement>();
		
		/*_newElement.serializedObject = new UnityEditor.SerializedObject(_newElement);
		_newElement.serializedPropertyTimeAppear = _newElement.serializedObject.FindProperty("timeAppear");
		_newElement.serializedPropertyTimeDisappear = _newElement.serializedObject.FindProperty("timeDisappear");*/
	
        return _newElement.Confirm(_gameObjectRef, _newSortID, _isNested_IAM);
    }
    public virtual InterfaceAnmElement Confirm(GameObject _gameObjectRef, int _newSortID, bool isNested_IAM) {
        this.gameObjectRef = _gameObjectRef;
        this.sortID = _newSortID;
        this.isNested_IAM = isNested_IAM;
        this.recycling = false;
        if (!this.isNested_IAM) {
            this.animator = _gameObjectRef.GetComponent<Animator>();
        }
        if (!list.Contains(this)) {
            list.Add(this);
        }
        return this;
    }
    public void UpdateProperties() {
        //Debug.Log(this.gameObjectRef.name);
        if (this.gameObjectRef.GetComponent<InterfaceAnimManager>() && this.gameObjectRef.transform.parent.GetComponent<InterfaceAnimManager>()) {
            this.isNested_IAM = true;
            this.animator = null;
        } else {
            this.isNested_IAM = false;
            this.animator = this.gameObjectRef.GetComponent<Animator>();
			/*if (this.serializedObject==null){
				this.serializedObject = new UnityEditor.SerializedObject(this);
				this.serializedPropertyTimeAppear = this.serializedObject.FindProperty("timeAppear");
				this.serializedPropertyTimeDisappear = this.serializedObject.FindProperty("timeDisappear");
			}*/
        }
        #if UNITY_EDITOR
        animClips = AnimationUtility.GetAnimationClips(gameObjectRef);
        #endif
    }
    public virtual void Delete() {
        this.gameObjectRef = null;
        this.sortID = 0;
        this.timeAppear = 1;
        this.timeDisappear = 1;
        this.isNested_IAM = false;
        this.currentState = CSFHIAnimableState.disappeared;
        this.recycling = true;
        //DestroyImmediate(this,false);
    }
    public void Update() {
        if (!this.gameObjectRef || this.gameObjectRef.GetComponent<InterfaceAnimManager>()) {
            //DestroyImmediate(this, false);
        }
    }
	/*public void OnEnable (){
		if (this.gameObjectRef && this.gameObjectRef.transform.parent)
			return;
		save serialized data, needed for prefabs version of IAM
		hideFlags = HideFlags.HideAndDontSave;
	}*/
}
