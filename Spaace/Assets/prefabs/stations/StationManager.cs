using UnityEngine;
using System.Collections;

public class StationManager : MonoBehaviour {
	public GameObject[] stores;

	void Start () {
		ItemManager.addItems();
		spawnStations();
	}
	
	void Update () {
		
	}
	bool checkCollisions(Vector2 pos){
		string[] tags = {"store","abdstation"};
		for(int i=0;i<tags.Length;i++){
			GameObject[] tempStations = GameObject.FindGameObjectsWithTag(tags[i]);
			for(int k=0;k<tempStations.Length;k++){
				if(Vector2.Distance(pos, new Vector2(tempStations[k].transform.position.x,tempStations[k].transform.position.y)) < 10){
					return true;
				}
			}
		}
		return false;
	}
	void spawnStations(){
		Sector[] sectors = this.gameObject.GetComponentsInChildren<Sector>();
		for(int i=0;i<sectors.Length;i++){
			Sector sec = sectors[i];
			int stationCount = sec.stationCount;
			for(int j=0;j<stationCount;j++){
				Vector2 pos = Vector2.zero;
				while(pos.magnitude < sec.innerLimit){
					pos = Random.insideUnitCircle*sec.outerLimit;
					if(checkCollisions(pos)){
						pos = Vector2.zero;
						continue;
					}
				}
				Vector3 spawnPosition = new Vector3(pos.x,pos.y,0);
				int s =  Random.Range(0,sec.stations.Length);
				GameObject newStation = (GameObject)Instantiate(sec.stations[s],spawnPosition,new Quaternion(0,0,0,0));
				newStation.tag = "abdstation";
				newStation.GetComponent<AbandonedStation>().setSector(i);
			}	

			int storeCount = sec.storeCount;
			for(int j=0;j<storeCount;j++){
				Vector2 pos = Vector2.zero;
				while(pos.magnitude < sec.innerLimit){
					pos = Random.insideUnitCircle*sec.outerLimit;
					if(checkCollisions(pos)){
						pos = Vector2.zero;
						continue;
					}
				}
				Vector3 spawnPosition = new Vector3(pos.x,pos.y,0);
				int s =  Random.Range(0,stores.Length);
				GameObject newStore = (GameObject)Instantiate(stores[s],spawnPosition,new Quaternion(0,0,0,0));
				newStore.tag = "store";
				newStore.GetComponent<Store>().setSector(i);
			}	
		}
	}
}
