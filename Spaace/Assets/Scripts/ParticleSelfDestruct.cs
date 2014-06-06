using UnityEngine;
using System.Collections;

public class ParticleSelfDestruct : MonoBehaviour {
	
	public float time = 5;
	void Start () {
		Destroy(this.gameObject, time);
	}
}
