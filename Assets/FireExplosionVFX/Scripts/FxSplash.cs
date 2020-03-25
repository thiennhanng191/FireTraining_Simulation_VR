using UnityEngine;
using System.Collections;

public class FxSplash : MonoBehaviour {
	private Rigidbody rb;
	public ParticleSystem Ps_Splash;
	public ParticleSystem Ps_Trail;
	
	void Start() {
		rb = this.GetComponent<Rigidbody>();
		mh = this.GetComponent<MeshRenderer>();
	}

	void OnCollisionEnter(Collision collision) {
		// Debug-draw all contact points and normals
		mh.enabled = false;
		Ps_Trail.Stop();
		Ps_Splash.Play();
		Invoke("DestroyFx",2f);
	}

	void DestroyFx()
	{
		Destroy(this.gameObject);
	}
	private MeshRenderer mh;
}
