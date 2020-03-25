using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LaunchEffect : MonoBehaviour {

	public float thrust;
	public Vector3 Dir;
	private Rigidbody rb;
	private ParticleSystem Ps;
	public GameObject FxPrefabLOW, FxPrefabHIGH;
	public GameObject Anim;

	/*********************************** MONOBEHAVIOUR ***********************************************/

	void Start() {

		//Anim.Play("FadeOff");
		Invoke("HideSplash",2.5f);
	}


	/*********************************** PUBLIC ***********************************************/

	public void LaunchSlow(int _Quality)
	{
		Time.timeScale = 0.5f;
		Launch(_Quality);
	}

	public void LaunchNormal(int _Quality)
	{
		Time.timeScale = 1f;
		Launch(_Quality);
	}

	/*********************************** PRIVATE ***********************************************/

	private void Launch(int _Quality)
	{		
		if(_Quality == 0 )
		{
			GameObject fx = Instantiate(FxPrefabLOW, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			fx.transform.parent = this.gameObject.transform;
			fx.transform.localPosition = Vector3.zero;
			fx.transform.localRotation = Quaternion.Euler(320,-90,0);
			rb = fx.GetComponent<Rigidbody>();
			mh = fx.GetComponent<MeshRenderer>();
			LaunchFX();
		}
		else
		{
			GameObject fx = Instantiate(FxPrefabHIGH, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			fx.transform.parent = this.gameObject.transform;
			fx.transform.localPosition = Vector3.zero;
			fx.transform.localRotation = Quaternion.Euler(320,-90,0);
			rb = fx.GetComponent<Rigidbody>();
			mh = fx.GetComponent<MeshRenderer>();
			LaunchFX();

		}
	}

	private void LaunchFX() 
	{
		mh.enabled = true;
		rb.AddForce(Dir * thrust);
	}

	private void HideSplash()
	{
		Anim.SetActive(false);

	}

	private MeshRenderer mh;
}
