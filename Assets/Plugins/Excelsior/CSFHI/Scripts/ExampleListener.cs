using UnityEngine;
using System.Collections;

public class ExampleListener : MonoBehaviour {
    public InterfaceAnimManager IAM;
    void Awake() {
        if (!IAM)
        return;
        AddListener();
    }
    void AddListener() {
        // For the "event" type, + and - operators have been overloaded. "+" adds
        // a method reference to the list of methods to call when the event is invoked.
        // "-" removes the reference from the list.
        IAM.OnStartAppear += HandleOnStartAppear;
        IAM.OnStartDisappear += HandleOnStartDisappear;
        IAM.OnEndAppear += HandleOnEndAppear;
        IAM.OnEndDisappear += HandleOnEndDisappear;
    }
    void RemoveListener() {
        IAM.OnStartAppear -= HandleOnStartAppear;
        IAM.OnStartDisappear -= HandleOnStartDisappear;
        IAM.OnEndAppear -= HandleOnEndAppear;
        IAM.OnEndDisappear -= HandleOnEndDisappear;
    }
    void HandleOnStartAppear(InterfaceAnimManager _IAM) {
        Debug.Log("EVENT LISTENER : " + _IAM.name + " onStartAppear");
    }
    void HandleOnStartDisappear(InterfaceAnimManager _IAM) {
        Debug.Log("EVENT LISTENER : " + _IAM.name + " onStartDisappear");
    }
    void HandleOnEndAppear(InterfaceAnimManager _IAM) {
        Debug.Log("EVENT LISTENER : " + _IAM.name + " onEndAppear");
    }
    void HandleOnEndDisappear(InterfaceAnimManager _IAM) {
        Debug.Log("EVENT LISTENER : " + _IAM.name + " onEndDisappear");
    }
	
}
