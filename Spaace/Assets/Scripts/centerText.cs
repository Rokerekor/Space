using UnityEngine;
using System.Collections;

public class centerText : MonoBehaviour {

	float time = 0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(time > 0){
			time -= Time.deltaTime;
		}else{
			this.GetComponent<TextMesh>().text = "";
		}
	}
	public void showText(string text,float time){
		this.GetComponent<TextMesh>().text = text;
		this.time = time;
	}
}
