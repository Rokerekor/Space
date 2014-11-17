using UnityEngine;
using System.Collections;

public class DroppedItemManager : MonoBehaviour {
	public GameObject textPop;
	public GameObject[] items;

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
	

	void setIcon(){
		this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(iconPath[value]);
	}

	public void dropItem(GameObject station){
		GameObject toDrop = items[Random.Range(0,items.Length)];
		GameObject drop = (GameObject)Instantiate(toDrop,station.transform.position,new Quaternion(0,0,0,0));
	}

}
