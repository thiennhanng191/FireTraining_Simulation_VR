using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
[ExecuteInEditMode]

//EXCELSIOR SCRIPT

[CustomEditor(typeof(InterfaceAnimManager)), CanEditMultipleObjects]
public class InterfaceAnimManagerEditor : Editor {

    private InterfaceAnimManager _target;
    void OnEnable() {
        _target = (InterfaceAnimManager)target;
    }
    public override void OnInspectorGUI() {
        _target.UpdateAnimClips();

        //for debug purpose only
        /*
        GUI.color = Color.red;
        if (GUILayout.Button("Reset Element List")) {
            _target.elementsList.Clear();
        }
        GUI.color = Color.white;*/

		if (Application.isPlaying){ //checking if we didn't selected a prefab
			if (_target.forceUnscaledTime) {
				EditorGUILayout.LabelField ("Is forcing unscaled time");
			}
			GUILayout.BeginHorizontal();
            GUI.color = Color.cyan;

            if (_target.currentState == CSFHIAnimableState.disappearing || _target.currentState == CSFHIAnimableState.appearing)
                GUI.enabled = false;
			
            if (GUILayout.Button("Appear"))
                _target.startAppear();
			
			GUI.enabled = _target.currentState != CSFHIAnimableState.disappearing;
            GUI.color = Color.yellow;

            if (GUILayout.Button("Disappear"))
                _target.startDisappear();
			
		} else {
			GUILayout.BeginHorizontal();
			_target.forceUnscaledTime = GUILayout.Toggle(_target.forceUnscaledTime, new GUIContent("Unscaled Time"));
			GUI.enabled = true;
            if (_target.nestedIAM.Count >= 1) {
                GUI.color = Color.green;
                if (GUILayout.Button("Recompute Nested IAM")) {
                    _target.UpdateAnimClips();
                    for (int a = _target.nestedIAM.Count + 1; a > 0; a--) { //we need to do it twice for each IAM
                        foreach (InterfaceAnimManager _IAM in _target.nestedIAM) {
                            _IAM.UpdateAnimClips();
                        }
                    }
                    _target.UpdateAnimClips();
                    Debug.Log("CSFHI : AIM data for " + _target.gameObject.name + " has been recomputed");
                }
            }
        }
        GUILayout.EndHorizontal();
        GUI.color = Color.white;
        GUI.enabled = true;

        if (_target.gameObject.scene.name != null  && Application.isPlaying) { //checking if we didn't selected a prefab
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
		bool nested=!_target.isIAM_Root();
        if (nested) {
            EditorGUILayout.LabelField("Nested IAM from " + _target.transform.parent.name, EditorStyles.boldLabel);
            GUI.enabled = false;
        } else {
            GUI.enabled = true;
            _target.testLoop = GUILayout.Toggle(_target.testLoop, new GUIContent("Appear/Disappear Loop"));
            EditorGUILayout.LabelField("Additional seconds between loops", EditorStyles.centeredGreyMiniLabel);
            GUI.enabled = _target.testLoop;
            _target.timeBetweenLoops = EditorGUILayout.IntSlider(_target.timeBetweenLoops, 1, 10);
            GUI.enabled = true;
        }

        if (nested)
            GUI.enabled = false;
		
        _target.autoStart = GUILayout.Toggle(_target.autoStart, new GUIContent("Auto start"));
		_target.useDelays = GUILayout.Toggle(_target.useDelays, new GUIContent("Use Custom Anim Delays"));

        if (nested) {
            GUI.enabled = true;
        }
        if (_target.useDelays) {

            if (nested)
                GUI.enabled = false;

            if (_target.nestedIAM.Count >= 1) {
                _target.waitAppear_Nested = GUILayout.Toggle(_target.waitAppear_Nested, new GUIContent("Wait nested IAM appeareance"));
                _target.waitDisappear_Nested = GUILayout.Toggle(_target.waitDisappear_Nested, new GUIContent("Wait nested IAM disappeareance"));
            }
            GUI.enabled = true;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Appear delays", EditorStyles.boldLabel);
            _target.autoLinearAppearDelay = GUILayout.Toggle(_target.autoLinearAppearDelay, new GUIContent("Linear"));
            DisplayElementList(true);

            if (_target.autoLinearAppearDelay) {
                int index = 0;
                foreach (InterfaceAnmElement _element in _target.elementsList) {
                    if (_element && _element.gameObjectRef) {//in case it has been destroyed in the meantime
                        if (index != _target.elementsList.Count - 1) {
                            _element.timeAppear = (Mathf.Floor((float)index * ((float)1 / (float)(_target.elementsList.Count - 1)) * 100)) / 100;
                        } else {
                            _element.timeAppear = 1;
                        }
                        index++;
                    }
                }
            }
            if (_target.invertDelays) {
                int index = _target.elementsList.Count;
                foreach (InterfaceAnmElement _element in _target.elementsList) {
                    if (_element && _element.gameObjectRef) {//in case it has been destroyed in the meantime
                        _element.timeDisappear = 1 - _element.timeAppear;
                        index--;
                    }
                }
            }
            if (_target.cloneDelays) {
                int index = _target.elementsList.Count;
                foreach (InterfaceAnmElement _element in _target.elementsList) {
                    if (_element && _element.gameObjectRef) {//in case it has been destroyed in the meantime
                        _element.timeDisappear = _element.timeAppear;
                        index--;
                    }
                }
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Disappear delays", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Reproduce appear delays", EditorStyles.centeredGreyMiniLabel);

            GUILayout.BeginHorizontal();
            if (_target.cloneDelays)
                GUI.enabled = false;

            _target.invertDelays = GUILayout.Toggle(_target.invertDelays, new GUIContent("Invert"), EditorStyles.toolbarButton);
            GUI.enabled = true;

            if (_target.invertDelays)
                GUI.enabled = false;

            _target.cloneDelays = GUILayout.Toggle(_target.cloneDelays, new GUIContent("Clone"), EditorStyles.toolbarButton);
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            DisplayElementList(false);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Sounds", EditorStyles.boldLabel);
            _target.openSound = EditorGUILayout.ObjectField("OpenSound:", _target.openSound, typeof(AudioClip), true) as AudioClip;
            _target.closeSound = EditorGUILayout.ObjectField("CloseSound:", _target.closeSound, typeof(AudioClip), true) as AudioClip;
            EditorGUILayout.Space();
        }
    }

    void DisplayElementList(bool isAppear) {
        EditorGUILayout.LabelField("Timeline length multiplier", EditorStyles.centeredGreyMiniLabel);
        if (isAppear) {
            _target.appearDelay_SpeedMultiplier = EditorGUILayout.Slider(_target.appearDelay_SpeedMultiplier, 0.1f, 10);
        } else {
            _target.disappearDelay_SpeedMultiplier = EditorGUILayout.Slider(_target.disappearDelay_SpeedMultiplier, 0.1f, 10);
        }
        GUI.enabled = true;
        EditorGUILayout.LabelField("Elements delays timeline", EditorStyles.centeredGreyMiniLabel);
        foreach (InterfaceAnmElement _element in _target.elementsList) {
            if (_element && _element.gameObjectRef) {//in case it has been destroyed in the meantime
                GUILayout.BeginHorizontal();

                if (GUILayout.Button(_element.gameObjectRef.name, EditorStyles.miniButton)) {
                    EditorGUIUtility.PingObject(_element.gameObjectRef);
                    Selection.activeObject = _element.gameObjectRef;
                    SceneView.FrameLastActiveSceneView();
                    Selection.activeObject = this._target;
                }
                if ((isAppear && _target.autoLinearAppearDelay) || (!isAppear && (_target.invertDelays || _target.cloneDelays))) {
                    GUI.enabled = false;
                }
				
				//_element.UpdateProperties();
                //_element.serializedObject.Update();

                EditorGUI.BeginChangeCheck();
				
				if (isAppear) {
                    _element.timeAppear = EditorGUILayout.Slider(_element.timeAppear, 0, 1, GUILayout.MaxWidth(140));
                } else {
                    _element.timeDisappear = EditorGUILayout.Slider(_element.timeDisappear , 0, 1, GUILayout.MaxWidth(140));
                }
                /*
				
                if (isAppear) {
                   _element.serializedPropertyTimeAppear.floatValue=EditorGUILayout.Slider(_element.serializedPropertyTimeAppear.floatValue,0f, 1f, GUILayout.MaxWidth(140));
                } else {
                   _element.serializedPropertyTimeDisappear.floatValue=EditorGUILayout.Slider(_element.serializedPropertyTimeDisappear.floatValue,0f, 1f, GUILayout.MaxWidth(140));
                }
                */
                Undo.RecordObject(target, "Change interface delay");
				
				//_element.serializedObject.ApplyModifiedProperties();
				
                GUI.enabled = true;
                GUILayout.EndHorizontal();
            }
        }
        GUI.enabled = true;
    }
}
#endif
