using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public GameObject map;
	public GameObject fog;
	public GameObject playerSprite;
	public Sprite abandonedSprite;
	public Sprite storeSprite;

	GameObject player;
	bool status = false;
	const int unitsValue = 14;
	const int spawnSize = 1;
	const int mapWidth = 100;
	float unitSize = mapWidth/unitsValue*2;

	GameObject[,] fogs = new GameObject[unitsValue,unitsValue];
	bool[,] explored = new bool[unitsValue,unitsValue];

	GameObject[] stores;
	GameObject[] stations;

	int timer = 0;

	void Start () {
		player =  GameObject.FindGameObjectWithTag("Player");;
		map.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
		startFog();
		startStations();
	}
	
	void Update () {
		timer++;
		if(timer > 10){
			checkExplored();
		}
	}
	void startStations(){
		//stores = new GameObject[GameObject.FindGameObjectsWithTag("store").Length];
		//stations = new GameObject[GameObject.FindGameObjectsWithTag("abdstation").Length];
		GameObject[] tempStores = GameObject.FindGameObjectsWithTag("store");
		GameObject[] tempStations = GameObject.FindGameObjectsWithTag("abdstation");
		stores = new GameObject[tempStores.Length];
		stations = new GameObject[tempStations.Length];
		for(int i=0;i<tempStores.Length;i++){
			float yVal = Camera.main.camera.orthographicSize;
			GameObject store = tempStores[i];
			Vector3 pos = new Vector3(store.transform.position.x/mapWidth * yVal,store.transform.position.y/mapWidth * yVal,0);
			GameObject newStore = (GameObject)Instantiate(storeSprite,pos,new Quaternion(0,0,0,0));
			newStore.GetComponent<SpriteRenderer>().sortingOrder = 12;
			newStore.SetActive(false);
			newStore.transform.parent = Camera.main.transform;
			stores[i] = newStore;
		}
		for(int i=0;i<tempStations.Length;i++){
			float yVal = Camera.main.camera.orthographicSize;
			GameObject station = tempStations[i];
			Vector3 pos = new Vector3(station.transform.position.x/mapWidth * yVal,station.transform.position.y/mapWidth * yVal,0);
			GameObject newStation = (GameObject)Instantiate(abandonedSprite,pos,new Quaternion(0,0,0,0));
			newStation.GetComponent<SpriteRenderer>().sortingOrder = 12;
			newStation.SetActive(false);
			newStation.transform.parent = Camera.main.transform;
			stations[i] = newStation;
		}
	}
	void startFog(){
		for(int i=(unitsValue/2)-spawnSize;i<(unitsValue/2)+spawnSize;i++){
			for(int k=(unitsValue/2)-spawnSize;k<(unitsValue/2)+spawnSize;k++){
				explored[i,k] = true;
			}
		}
		for(int i=0;i<unitsValue;i++){
			float yVal = Camera.main.camera.orthographicSize * 2 / unitsValue;
			float xVal = yVal*Camera.main.aspect;
			for(int k=0;k<unitsValue;k++){
				Vector3 position = new Vector3(i*yVal - (unitsValue/2-0.5f)*yVal,k*yVal - (unitsValue/2-0.5f)*yVal,1);
				position+=Camera.main.transform.position;
				GameObject newFog = (GameObject)Instantiate(fog,position,map.transform.rotation);
				newFog.transform.parent = Camera.main.transform;
				//newFog.transform.localScale =  new Vector3(newFog.transform.localScale.x*20/unitsValue,newFog.transform.localScale.y*20/unitsValue,newFog.transform.localScale.y);
				newFog.SetActive(false);
				fogs[i,k] = newFog;
			}
		}
	}
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.M){
					openMap();
				}
			}else if(e.type == EventType.KeyUp){
				if(e.keyCode == KeyCode.M){
					closeMap();
				}
			}
		}
	}
	void checkExplored(){
		Vector3 playerPos = player.transform.position;
		int xVal = Mathf.FloorToInt((playerPos.x)/(unitSize)) + unitsValue/2;
		int yVal = Mathf.FloorToInt((playerPos.y)/(unitSize)) + unitsValue/2;	
		if(explored[xVal,yVal] == false){
			Debug.Log("EXPLORED");
		}
		explored[xVal,yVal] = true;
	}
	void openMap(){
		if(!status){
			status = true;
			Time.timeScale = 0;
			map.SetActive(true);
			for(int i=0;i<unitsValue;i++){			
				for(int k=0;k<unitsValue;k++){
					GameObject f = fogs[i,k];
					f.SetActive(!explored[i,k]);
				}
			}
			float yVal = Camera.main.camera.orthographicSize;
			Vector3 pos = new Vector3(player.transform.position.x/mapWidth * yVal + Camera.main.transform.position.x,player.transform.position.y/mapWidth * yVal + Camera.main.transform.position.y,0);
			playerSprite.SetActive(true);
			playerSprite.transform.position = pos;
			playerSprite.transform.rotation = player.transform.rotation;

			foreach(GameObject store in stores){
				store.SetActive(true);
			}
			foreach(GameObject station in stations){
				station.SetActive(true);
			}
//			foreach(GameObject store in stores){
//				float hVal = Camera.main.camera.orthographicSize * 2 / unitsValue;
//				float wVal = hVal*Camera.main.aspect;
//				int xVal = Mathf.FloorToInt((store.transform.position.x - Camera.main.transform.position.x)/wVal) + unitsValue/2;
//				int yVal = Mathf.FloorToInt((store.transform.position.y - Camera.main.transform.position.y)/hVal) + unitsValue/2;
//				Debug.Log("X: " + xVal);
//				Debug.Log("Y: " + yVal);
//				if(explored[xVal,yVal]){
//					store.SetActive(true);
//				}
//			}
//
//			foreach(GameObject station in stations){
//				float hVal = Camera.main.camera.orthographicSize * 2 / unitsValue;
//				float wVal = hVal*Camera.main.aspect;
//				int xVal = Mathf.FloorToInt((station.transform.position.x - Camera.main.transform.position.x)/wVal) + unitsValue/2;
//				int yVal = Mathf.FloorToInt((station.transform.position.y - Camera.main.transform.position.y)/hVal) + unitsValue/2;
//				Debug.Log("X: " + xVal);
//				Debug.Log("Y: " + yVal);
//				if(explored[xVal,yVal]){
//					station.SetActive(true);
//				}
//			}
		}
	}
	void closeMap(){
		Time.timeScale = 1;
		map.SetActive(false);
		for(int i=0;i<unitsValue;i++){			
			for(int k=0;k<unitsValue;k++){
				GameObject f = fogs[i,k];
				f.SetActive(false);
			}
		}
		foreach(GameObject store in stores){
			store.SetActive(false);
		}
		foreach(GameObject station in stations){
			station.SetActive(false);
		}
		playerSprite.SetActive(false);
		status = false;
	}
}
