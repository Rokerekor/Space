using UnityEngine;
using System.Collections;

public class ConvoyManager : MonoBehaviour {
	public GameObject convoy;
	float timer;
	void Start () {
		timer = 15;
	}
	
	// Update is called once per frame
	void Update () {
		timeFunc();
	}
	void timeFunc(){
		timer -= Time.deltaTime;
		if(timer <=0){
			checkStations();
		}
	}
	void checkStations(){
		GameObject[] stations = GameObject.FindGameObjectsWithTag("abdstation");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float distance = Mathf.Infinity;
		int closest = -1;
		for(int i=0;i<stations.Length;i++){
			if(!stations[i].GetComponent<AbandonedStation>().isActive() && !stations[i].GetComponent<AbandonedStation>().isCleared()){
				float temp = Vector3.Distance(stations[i].transform.position, player.transform.position);
				if(temp < distance){
					distance = temp;
					closest = i;
				}
			}
		}
		if(distance < 50){
			spawnConvoy(stations[closest]);
			timer = 45;
		}else{
			timer = 10;
		}
	}
	void spawnConvoy(GameObject station){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 distance = Vector3.Normalize(player.transform.position - station.transform.position)*15;
		Vector3 position = GameObject.FindGameObjectWithTag("Player").transform.position + distance;
		GameObject newConvoy = (GameObject)Instantiate(convoy,position,new Quaternion(0,0,0,0));
		newConvoy.GetComponent<Convoy>().setStation(station);
	}
}
