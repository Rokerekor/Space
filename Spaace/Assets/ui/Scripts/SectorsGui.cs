using UnityEngine;
using System.Collections;

public class SectorsGui : MonoBehaviour {
	int timer = 0;

	void Update () {
		timer++;
		if(timer > 10){
			updateText();
			timer = 0;
		}
	}
	void updateText(){
		this.guiText.text = "Sector: " + GameObject.FindGameObjectWithTag("sectorManager").GetComponent<SectorsManager>().getSector();
	}
}
