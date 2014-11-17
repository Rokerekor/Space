using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	public GameObject fog;
	public GameObject abandonedSprite;
	public GameObject storeSprite;
	public GameObject unknownSprite;
	public GameObject map;

	GameObject player;
	GameObject playerSprite;


	bool status = false;
	const int unitsValue = 28;
	const int spawnSize = 1;
	float mapWidth = 200f/6f;

	GameObject[,] fogs = new GameObject[unitsValue,unitsValue];
	bool[,] explored = new bool[unitsValue,unitsValue];

	List<GameObject> stores = new List<GameObject>();
	List<GameObject> stations = new List<GameObject>();

	int timer = 0;

	void Start () {
		player =  GameObject.FindGameObjectWithTag("Player");
		playerSprite = map.transform.FindChild("PlayerSprite").gameObject;
		//playerSprite.transform.localScale = playerSprite.transform.localScale/(mapWidth/50);
//		startFog();
		//startStations();
	}
	
	void Update () {
		/*timer++;
		if(timer > 10){
			checkExplored();
		}*/
	}
	void startStations(){
		/*GameObject[] tempStores = GameObject.FindGameObjectsWithTag("store");
		GameObject[] tempStations = GameObject.FindGameObjectsWithTag("abdstation");
		stores = new GameObject[tempStores.Length];
		stations = new GameObject[tempStations.Length];
		SectorsManager smScript = GameObject.FindGameObjectWithTag("sectorManager").GetComponent<SectorsManager>();
		int secExplored = smScript.getExploredSector();
		for(int i=0;i<tempStores.Length;i++){
			GameObject store = tempStores[i];
			Store sScript = store.GetComponent<Store>();
			sScript.setID(i);
			if(sScript.getSector() <= secExplored){
				Sector[] sectorsArray = GameObject.FindGameObjectWithTag("sectorManager").GetComponentsInChildren<Sector>();
				Vector3 position = new Vector3(store.transform.position.x/mapWidth,store.transform.position.y/mapWidth,0.9f);
				GameObject newStore = (GameObject)Instantiate(unknownSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
				newStore.transform.parent = map.transform;
				newStore.transform.localPosition = position;
				stores[i] = newStore;
			}
		}
		for(int i=0;i<tempStations.Length;i++){
			GameObject station = tempStations[i];
			AbandonedStation sScript = station.GetComponent<AbandonedStation>();
			sScript.setID(i);
			if(sScript.getSector() <= secExplored){
				Sector[] sectorsArray = GameObject.FindGameObjectWithTag("sectorManager").GetComponentsInChildren<Sector>();
				Vector3 position = new Vector3(station.transform.position.x/mapWidth,station.transform.position.y/mapWidth,0.9f);
				GameObject newStation = (GameObject)Instantiate(unknownSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
				newStation.transform.parent = map.transform;
				newStation.transform.localPosition = position;
				stations[i] = newStation;
			}
		}	*/
	}
	public void stationClear(GameObject station){
		/*for(int i=0;i<stations.Count;i++){
			//USE THIS FIRST VERSION IF STATIONS ARE ADDED BEFORE CLEARED
			if(script.getID() == i){
				GameObject s = stations[i];
				GameObject newStation = (GameObject)Instantiate(abandonedSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
				newStation.transform.parent = map.transform;
				newStation.transform.localPosition = s.transform.localPosition;
				Destroy(s);
				stations[i] = newStation;
				return;
			}
		}*/
		AbandonedStation sScript = station.GetComponent<AbandonedStation>();
		sScript.setID(stations.Count);
		Vector3 position = new Vector3(station.transform.position.x/mapWidth,station.transform.position.y/mapWidth,0.9f);
		GameObject newStation = (GameObject)Instantiate(abandonedSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
		newStation.transform.parent = map.transform;
		newStation.transform.localPosition = position;
		stations.Add(newStation);
	}
	public void stationFound(GameObject station){
		AbandonedStation sScript = station.GetComponent<AbandonedStation>();
		sScript.setID(stations.Count);
		Vector3 position = new Vector3(station.transform.position.x/mapWidth,station.transform.position.y/mapWidth,0.9f);
		GameObject newStation = (GameObject)Instantiate(unknownSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
		newStation.transform.parent = map.transform;
		newStation.transform.localPosition = position;
		stations.Add(newStation);
	}
	public void storeFound(Store script){
		GameObject store = script.gameObject;
		script.setID(stores.Count);
		Vector3 position = new Vector3(store.transform.position.x/mapWidth,store.transform.position.y/mapWidth,0.9f);
		GameObject newStore = (GameObject)Instantiate(storeSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
		newStore.transform.parent = map.transform;
		newStore.transform.localPosition = position;
		stores.Add(newStore);
		/*for(int i=0;i<stores.Length;i++){
			if(script.getID() == i){
				GameObject s = stores[i];
				GameObject newStore = (GameObject)Instantiate(storeSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
				newStore.transform.parent = map.transform;
				newStore.transform.localPosition = s.transform.localPosition;
				Destroy(s);
				stores[i] = newStore;
				return;
			}
		}*/
	}
	/*void startFog(){
		for(int i=0;i<unitsValue;i++){
			float yVal = Camera.main.camera.orthographicSize * 2 / unitsValue;
			float xVal = yVal*Camera.main.aspect;
			for(int k=0;k<unitsValue;k++){
				Vector3 position = new Vector3((float)i/unitsValue*aspect + 0.25f,(float)k/unitsValue + 0.5f/unitsValue,11);
				position+=Camera.main.transform.position;
				GameObject newFog = (GameObject)Instantiate(fog,new Vector3(0,0,0),new Quaternion(0,0,0,0));
				newFog.transform.parent = map.transform;
				newFog.transform.localPosition = position;
				//newFog.transform.localScale =  new Vector3(1f/unitsValue*aspect,1f/unitsValue,0);
				fogs[i,k] = newFog;
			}
		}
		for(int i=(unitsValue/2)-spawnSize;i<(unitsValue/2)+spawnSize;i++){
			for(int k=(unitsValue/2)-spawnSize;k<(unitsValue/2)+spawnSize;k++){
				Destroy(fogs[i,k]);
			}
		}
	}*/
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.Tab){
					openMap();
				}
			}else if(e.type == EventType.KeyUp){
				if(e.keyCode == KeyCode.Tab){
					closeMap();
				}
			}
		}
	}
/*	void checkExplored(){
		Vector3 playerPos = player.transform.position;
		int xVal = Mathf.FloorToInt((playerPos.x)/(unitSize)) + unitsValue/2;
		int yVal = Mathf.FloorToInt((playerPos.y)/(unitSize)) + unitsValue/2;	
		if(explored[xVal,yVal] == false){
			//Debug.Log("EXPLORED");
		}
		Destroy(fogs[xVal,yVal]);
		explored[xVal,yVal] = true;
	}*/
	void openMap(){
		if(!status){
			status = true;

			Time.timeScale = 0;
			map.SetActive(true);

			Camera.main.orthographicSize = 6f;

			SectorsManager smScript = GameObject.FindGameObjectWithTag("sectorManager").GetComponent<SectorsManager>();
			int secExplored = smScript.getExploredSector();
			Sector[] sectorsArray = GameObject.FindGameObjectWithTag("sectorManager").GetComponentsInChildren<Sector>();
			float val = sectorsArray[secExplored].outerLimit;


			playerSprite.transform.localPosition = new Vector3(player.transform.position.x/mapWidth,player.transform.position.y/mapWidth,1);
			playerSprite.transform.rotation = player.transform.rotation;

		}
	}
	void closeMap(){
		if(status){
			status = false;
			Time.timeScale = 1;
			map.SetActive(false);
		}
	}
}
