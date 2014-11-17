using UnityEngine;
using System.Collections;

public class AbilityIconFunc : MonoBehaviour {
	public Sprite sprite;
	public void setKey(int key){
		if(key == 0){
			GameObject.FindGameObjectWithTag("GUI").transform.FindChild("ability1").GetComponent<SpriteRenderer>().sprite = sprite;
		}else if(key == 1){
			GameObject.FindGameObjectWithTag("GUI").transform.FindChild("ability2").GetComponent<SpriteRenderer>().sprite = sprite;
		}else if(key == 2){
			GameObject.FindGameObjectWithTag("GUI").transform.FindChild("ability3").GetComponent<SpriteRenderer>().sprite = sprite;
		}else if(key == 3){
			GameObject.FindGameObjectWithTag("GUI").transform.FindChild("ability4").GetComponent<SpriteRenderer>().sprite = sprite;
		}
	}
}