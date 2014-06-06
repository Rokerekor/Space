using UnityEngine;
using System.Collections;

public class coords : MonoBehaviour {
	public bool xory;
	int timer = 0;

	void Start () {
	
	}
	
	void Update () {
		timer++;
		if(timer > 10){
			updateText();
			timer = 0;
		}
	}
	void updateText(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(xory == true){
			this.guiText.text = "x: " + Mathf.RoundToInt(player.transform.position.x);
		}else{
			this.guiText.text = "y: " + Mathf.RoundToInt(player.transform.position.y);
		}
	}
}
