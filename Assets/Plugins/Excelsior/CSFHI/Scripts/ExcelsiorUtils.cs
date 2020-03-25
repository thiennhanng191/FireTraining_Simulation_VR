using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//EXCELSIOR SCRIPT - Collections of tools used for the holo interface examples showroom

[AddComponentMenu("Excelsior/ExcelsiorUtils")]
public class ExcelsiorUtils:MonoBehaviour {
    public GameObject target;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Text textContent;
    public Slider slider;
	// Use this for initialization
	void Start () {
	
	}
    private void playSound() {
        if (audioSource && audioSource.enabled)
            audioSource.clip = audioClip;
            audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetPosition(int _scaleValue) {
        target.transform.position = Vector3.one * _scaleValue;
        playSound();
    }
    public void SetPositionAndScaleTo(GameObject _object) {
        target.transform.localPosition = _object.transform.localPosition * -1;
        target.transform.localScale = _object.transform.localScale;
        playSound();
    }
    public void SetUniformScale(int _scaleValue) {
        target.transform.localScale = Vector3.one * _scaleValue;
        playSound();
    }
    public void SetUniformWidthHeight(int _widthHeightValue) {
        target.GetComponent<Image>().rectTransform.sizeDelta = Vector3.one * _widthHeightValue;
        playSound();
    }
    public void SetImageColor(int _colorID) {
        Color32 _color;
        switch (_colorID) {
            case 1:
                _color = Color.blue;
                break;
            case 2:
                _color = Color.red;
                break;
            default:
                _color = Color.yellow;
                break;
        }
        target.GetComponent<Image>().color = _color;
        playSound();
    }
    public void OpenWebPage() {
        Application.OpenURL("http://www.assetstore.unity3d.com/#!/content/69794");
        playSound();
    }
    public void GetSliderValue_ToText() {
        textContent.text = slider.value.ToString();
    }
}
