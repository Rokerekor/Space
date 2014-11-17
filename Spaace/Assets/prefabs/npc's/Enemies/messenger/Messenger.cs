using UnityEngine;
using System.Collections;

public class Messenger : MonoBehaviour {
	BaseEnemy be;
	void Start () {
		be = this.GetComponent<BaseEnemy>();
		relocate();
	}
	void Update () {
		checkDestroyed();
		checkStation();
	}
	public void relocate(){
		GameObject[] stations = GameObject.FindGameObjectsWithTag("abdstation");
		GameObject tempStation = be.getStation();
		while(tempStation == be.getStation()){
			int val = Random.Range(0,stations.Length);
			if(!stations[val].GetComponent<AbandonedStation>().isCleared()){
				be.setStation(stations[val]);
			}
		}
	}
	void checkStation(){
		float distance = Vector3.Distance(this.transform.position,be.getStation().transform.position);
		if(distance < 5){
			relocate();
		}
	}
	void checkDestroyed(){
		if(this.GetComponent<BaseEnemy>().hull <= 0){
			//Tell player the station is found
			GameObject gui = GameObject.FindGameObjectWithTag("GUI");
			gui.GetComponentInChildren<centerText>().showText("Messenger destroyed. Destination coordinates added to map.",5);
			Camera.main.GetComponent<Map>().stationFound(be.getStation());
		}
	}
}
