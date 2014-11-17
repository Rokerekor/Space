using UnityEngine;
using System.Collections;

public class cameraFunctions : MonoBehaviour {
	
	bool fading = false;
	Color colour;
	float time;
	float counter = 0;
	float fadeSpeed;
	void Start(){
	}
	void Update(){
		if(fading){
			this.GetComponent<SpriteRenderer>().color = Color.Lerp(this.GetComponent<SpriteRenderer>().color, colour, fadeSpeed);
		}
	}
	public void fadeToBlack(float fadeSpeed){
		fading = true;
		this.fadeSpeed = fadeSpeed;
		colour = Color.black;
	}
	public void fadeToClear(float fadeSpeed){
		fading = true;
		this.fadeSpeed = fadeSpeed;
		colour = Color.clear;
	}
}
