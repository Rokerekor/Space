using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	void Start () {
		this.gameObject.transform.FindChild("Cam").transform.FindChild("GUI").gameObject.SetActive (true);
		this.gameObject.transform.FindChild("Cam").gameObject.SetActive(true);
		this.gameObject.transform.FindChild("Starfield").gameObject.SetActive(true);
		this.gameObject.transform.FindChild("Player").gameObject.SetActive(true);
	}
	
	void Update () {
	
	}
}
