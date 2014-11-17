using UnityEngine;
using System.Collections;

public class DamageNumber : MonoBehaviour {

	Color colour;
	void Start () {
		colour = this.GetComponent<TextMesh>().color;
	}
	
	// Update is called once per frame
	void Update () {
		colour = new Color(colour.r,colour.g,colour.b,colour.a-0.02f);
		this.GetComponent<TextMesh>().color = colour;
		if(colour.a <= 0){
			Destroy(gameObject);
		}
	}
}
