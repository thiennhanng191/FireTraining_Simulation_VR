using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

//EXCELSIOR SCRIPT

[AddComponentMenu("Excelsior/CSFHI/Example UI")]
public class ExampleUI : MonoBehaviour {
    public Text displayedName;
    private InterfaceAnimManager current;
    private int indexInterface = 0;
    public InterfaceAnimManager[] holoInterfaceList;
    public ExampleCam currentCam;
    public string searchTarget = "HoloInterfaces";
    public string searchCam = "currentCam";
    private int indexScene = 0;
    private static bool allDelaysStatus = true;
    private static bool allImgEffectsStatus = true;
    public List<string> loadableScenes = new List<string>();
    public GameObject fullscreenSwith;
    public AudioSource audioSource;

    void Start() {
        if (holoInterfaceList[indexInterface] != null)
            CallInterface(holoInterfaceList[indexInterface]);
    }
    private void playSound() {
        if (audioSource && audioSource.enabled)
            audioSource.Play();
    }
    public void DoOutFullscreen() {
        if (Screen.fullScreen)
            Screen.fullScreen = false;
        
        playSound();
    }
    public void DoChangeEnvironment() {
        indexScene++;
        if (indexScene >= loadableScenes.Count)
            indexScene = 0;
        playSound();
        SceneManager.LoadScene(loadableScenes[indexScene]);
        InitCam();
    }

    //Taking care of Unity OnLevelWasLoaded depreciation
	#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(Scene scene, LoadSceneMode mod) {
       // Debug.Log("OnSceneLoaded Removed");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        InitCam();
    }
	#else
	void OnLevelWasLoaded() {
		InitCam();
	}
	#endif
	
    public void Awake() {
	#if UNITY_5_4_OR_NEWER
        //Debug.Log("OnSceneLoaded Added");
        SceneManager.sceneLoaded += OnSceneLoaded;
	#endif
		//above, init cam is only called if the scene is dynamically loaded, so we add this here too:
		InitCam();
    }
    private void InitCam() {
        Debug.Log("InitCam + " + searchCam + " " + searchTarget);
        if (searchCam == "")
			return;
		
		currentCam = GameObject.Find(searchCam).GetComponent<ExampleCam>();
		if (currentCam && searchTarget != "" && currentCam.mouseOrbit.target==null) {
			currentCam.mouseOrbit.target = GameObject.Find(searchTarget).transform;
		}
		if (!allImgEffectsStatus) {
			foreach (MonoBehaviour _img in currentCam.ImageEffectsList) {
				_img.enabled = allImgEffectsStatus;
			}
		}
		if (!allDelaysStatus) {
			foreach (InterfaceAnimManager ia in holoInterfaceList) {
				ia.useDelays = allDelaysStatus;
			}
		}
        
    }
    public void Update() {
        fullscreenSwith.SetActive(Screen.fullScreen);
    }
    public void DoSwitchAutoCam() {
        currentCam.mouseOrbit.enabled = !currentCam.mouseOrbit.enabled;
    }
    public void DoSwitchCameraEffects() {
        allImgEffectsStatus = !allImgEffectsStatus;

        foreach (MonoBehaviour _img in currentCam.ImageEffectsList) {
            _img.enabled = allImgEffectsStatus;
        }
        if (audioSource && audioSource.enabled)
            audioSource.Play();
    }
    public void DoSwitchAnimDelays() {
        allDelaysStatus = !allDelaysStatus;
        foreach (InterfaceAnimManager ia in holoInterfaceList) {
            ia.useDelays = allDelaysStatus;
        }
        if (audioSource && audioSource.enabled)
            audioSource.Play();
    }
    public void DoAppear() {
        if (current) {
            current.startAppear();
            current.gameObject.SetActive(true);
        }
        playSound();
    }
    public void DoDisappear() {
        if (current)
            current.startDisappear();
                
        playSound();
    }
    public void DoNext() {
        indexInterface++;
        if (indexInterface >= holoInterfaceList.Length)
            indexInterface = 0;
        
        playSound();
        CallInterface(holoInterfaceList[indexInterface]);
    }
    public void DoPrevious() {
        indexInterface--;
        if (indexInterface < 0)
            indexInterface = holoInterfaceList.Length - 1;
        
        playSound();
        CallInterface(holoInterfaceList[indexInterface]);
    }
    private void CallInterface(InterfaceAnimManager _interface) {
        if (current)
            current.startDisappear(true);
                
        current = _interface;
        if (_interface) {
            current.gameObject.SetActive(true);
            current.startAppear();
            UpdateDisplayedInfo();
        }
    }
    public void UpdateDisplayedInfo() {
        if (current) {
            displayedName.text = current.name.ToString();
        } else {
            displayedName.text = "";
        }
    }
}