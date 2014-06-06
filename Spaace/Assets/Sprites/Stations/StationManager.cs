using UnityEngine;
using System.Collections;

public class StationManager : MonoBehaviour {
	public GameObject station;
	public GameObject[] stores;
	public GameObject[] abandonedStations;
	public int storeCount = 10;
	public int abandonedStationCount = 10;
	public int stageSize = 200;

	void Start () {
		ItemManager.addItems();
		spawnStations();
	}
	
	void Update () {
		
	}
	bool checkCollisions(int x, int y){
		string[] tags = {"store","abdstation"};
		for(int i=0;i<tags.Length;i++){
			GameObject[] tempStations = GameObject.FindGameObjectsWithTag(tags[i]);
			for(int k=0;k<tempStations.Length;k++){
				if(Mathf.Abs(x - tempStations[k].transform.position.x) < 10 && Mathf.Abs(y - tempStations[k].transform.position.y) < 10){
					return true;
				}
			}
		}
		return false;
	}
	void spawnStations(){
		for(int i=0;i<storeCount;i++){
			int x = 0;
			int y = 0;
			while(Mathf.Abs(x) < 20 && Mathf.Abs(y) < 15){
				x = Random.Range(-stageSize,stageSize);
				y = Random.Range(-stageSize,stageSize);
				if(checkCollisions(x,y)){
					x = 0;
					y = 0;
					continue;
				}
			}
			Vector3 spawnPosition = new Vector3(x,y,0);
			int s =  Random.Range(0,stores.Length);
			GameObject newStation = (GameObject)Instantiate(stores[s],spawnPosition,new Quaternion(0,0,0,0));
			newStation.tag = "store";
		}
		for(int i=0;i<abandonedStationCount;i++){
			int x = 0;
			int y = 0;
			while(Mathf.Abs(x) < 20 && Mathf.Abs(y) < 15){
				x = Random.Range(-stageSize,stageSize);
				y = Random.Range(-stageSize,stageSize);
				if(checkCollisions(x,y)){
					x = 0;
					y = 0;
					continue;
				}
			}
			Vector3 spawnPosition = new Vector3(x,y,0);
			int s =  Random.Range(0,abandonedStations.Length);
			GameObject newStation = (GameObject)Instantiate(abandonedStations[s],spawnPosition,new Quaternion(0,0,0,0));
			newStation.tag = "abdstation";
		}
	}
}
