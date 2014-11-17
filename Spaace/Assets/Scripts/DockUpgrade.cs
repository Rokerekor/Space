using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DockUpgrade : MonoBehaviour {

	public string upgrade;
	int level = 1;
	int cost = 0;

	List<TextMesh> textList = new List<TextMesh>();
	void Start () {
		setText();
		if(this.GetComponent<TextMesh>() != null){
			textList.Add(this.GetComponent<TextMesh>());
		}
		if(transform.Find("level") != null){
			textList.Add(transform.Find("level").GetComponent<TextMesh>());
		}
		if(transform.Find("cost") != null){
			textList.Add(transform.Find("cost").GetComponent<TextMesh>());
		}
	}
	void Update () {

	}
	void setText(){
		if(transform.Find("level") != null){
			transform.Find("level").GetComponent<TextMesh>().text = "LVL " + level;
		}
		transform.Find("cost").GetComponent<TextMesh>().text = cost.ToString();
	}
	void OnMouseDown(){
		if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().dockUpgrade(level,cost,upgrade)){
			level++;
			cost+=cost;
			setText();
		}
	}
	void OnMouseOver() {
		foreach(TextMesh mesh in textList){
			mesh.fontStyle = FontStyle.Bold;
		}
	}
	void OnMouseExit() {
		foreach(TextMesh mesh in textList){
			mesh.fontStyle = FontStyle.Normal;
		}
	}
}
