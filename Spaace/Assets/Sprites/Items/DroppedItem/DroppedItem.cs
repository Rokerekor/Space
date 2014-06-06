using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour {
	public GameObject textPop;

	int value = -1;
	int rotateVal = 1;
	string[] name = {"Cryo Rounds","Ion Rounds","Uranium Rounds","Locked and Loaded"};
	string[] iconPath = {"Icons/cryoround","Icons/ionround","Icons/uranium","Icons/loaded"};

	void Start () {
		value = Random.Range(0,name.Length);
		setIcon();
	}
	
	void Update () {
		this.transform.Rotate(0,0,rotateVal);
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.tag.Equals("Player")){
			giveItem();
		}
	}

	void setIcon(){
		this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(iconPath[value]);
	}
	void giveItem(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		PlayerScript script = player.GetComponent<PlayerScript>();
		if(value == 0){
			script.upgradeShip("cryo");
		}else if(value == 1){
			script.upgradeShip("ion");
		}else if(value == 2){
			script.increaseKnockback(1);
		}else if(value == 3){
			script.increaseLockedAndLoaded(1);
		}
		GameObject newPop =  (GameObject)Instantiate(textPop,this.transform.position,new Quaternion(0,0,0,0));
		newPop.GetComponent<TextMesh>().text = name[value] + " acquired!";
		Destroy(this.gameObject);
	}
}
