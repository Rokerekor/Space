using UnityEngine;
using System.Collections;

public class TriggerText : MonoBehaviour {
	public string triggerText = "sample text";
	public float xOffset = 0;
	public float yOffset = 0;
	float initialX;
	float initialY;
	bool triggered = false;
	void Start () {
		TextMesh display = GetComponent<TextMesh>();
		if(display == null){
			display = transform.Find("TextObject").GetComponent<TextMesh>();
		}
	}
	
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.tag.Equals("Player")){
			/*Store s = this.gameObject.transform.parent.GetComponent<Store>();
			if(s != null && !s.homeBase){
				Camera.main.GetComponent<Map>().storeFound(s);
			}*/
			textStatus(true);
			triggered = true;
		}
	}
	public void OnTriggerExit2D(Collider2D col){
		if(col.tag.Equals("Player")){
			textStatus(false);
			triggered = false;
		}
	}
	public void textStatus(bool status){
		TextMesh display = this.GetComponentInChildren<TextMesh>();
		display.transform.localPosition = new Vector3(xOffset,yOffset,0);
		display.alignment = TextAlignment.Left;
		display.anchor = TextAnchor.UpperLeft;
		if(status){
			display.text = triggerText;
		}else{
			display.text = " ";
		}
	}
	public void setText(string val){
		triggerText = val;
		if(triggered){
			textStatus(true);
		}
	}
	public bool isTriggered(){return triggered;}
}
