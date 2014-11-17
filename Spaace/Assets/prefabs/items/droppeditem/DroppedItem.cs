using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour {
	public GameObject textPop;
	public string name;
	int rotateVal = 1;

	void Update () {
		this.transform.Rotate(0,0,rotateVal);
	}
	
	public void pickedUp(){
		GameObject GUI = GameObject.FindGameObjectWithTag("GUI");
		ItemHolder ih = GUI.GetComponentInChildren<ItemHolder>();
		ih.addItem(this.gameObject.GetComponent<SpriteRenderer>().sprite);
		popText();
		Destroy(this.gameObject);
	}
	public void popText(){
		GameObject newPop =  (GameObject)Instantiate(textPop,this.transform.position,new Quaternion(0,0,0,0));
		newPop.GetComponent<TextMesh>().text = name + " acquired!";
	}
}
