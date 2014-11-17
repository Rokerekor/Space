using UnityEngine;
using System.Collections;

public class TextPop : MonoBehaviour {
	int delay = 50;
	void Start () {
	
	}

	void Update () {
		Color c = this.GetComponent<TextMesh>().color;
		if(delay > 0){
			delay--;
		}else{
			if(c.a > 0){
				this.GetComponent<TextMesh>().color = new Color(c.r,c.g,c.b,c.a-0.01f);
			}else{
				Destroy(this.gameObject);
			}
		}
	}
}
