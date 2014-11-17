using UnityEngine;
using System.Collections;

public class LightningArc : MonoBehaviour {
	Color color = Color.white;
	void Update () {
		color.a -= 0.1f;
		this.GetComponent<LineRenderer>().SetColors(color,color);
		if(color.a <= 0){
			Destroy(this.gameObject);
		}
	}
}
