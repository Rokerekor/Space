using UnityEngine;
using System.Collections;

public class Wreckagemovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = this.transform.position;

		position.x += 0.001f;
		position.y += 0.001f;

		this.transform.position = position;
	}
}
