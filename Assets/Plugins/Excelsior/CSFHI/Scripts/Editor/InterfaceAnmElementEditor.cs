using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]

//EXCELSIOR SCRIPT

[CustomEditor(typeof(InterfaceAnmElement)), CanEditMultipleObjects]
public class InterfaceAnmElementEditor : Editor {

    private InterfaceAnmElement _target;
    void OnEnable() {
        _target = (InterfaceAnmElement)target;
    }
    public override void OnInspectorGUI() {
        if (_target && _target.gameObjectRef && _target.gameObjectRef.transform.parent) {
            EditorGUILayout.LabelField("IAM Parent : " + _target.gameObjectRef.transform.parent.name);

            if (_target.gameObject.scene.name != null && Application.isPlaying) { //checking if we didn't selected a prefab
                    switch (_target.currentState) {
                        case CSFHIAnimableState.appearing:
                            GUI.color = Color.cyan;
                            break;
                        case CSFHIAnimableState.disappearing:
                            GUI.color = Color.yellow;
                            break;
                    }
                    EditorGUILayout.LabelField("Status : " + _target.currentState.ToString());
                    GUI.color = Color.white;
            }
        }
    }
}
#endif