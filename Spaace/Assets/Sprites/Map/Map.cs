using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public GameObject fog;
	public GameObject abandonedSprite;
	public GameObject storeSprite;
	public GameObject map;

	GameObject player;
	GameObject playerSprite;


	bool status = false;
	const int unitsValue = 14;
	const int spawnSize = 1;
	const int mapWidth = 100;
	float unitSize = mapWidth/unitsValue*2;
	float aspect = 9/16f;

	GameObject[,] fogs = new GameObject[unitsValue,unitsValue];
	bool[,] explored = new bool[unitsValue,unitsValue];

	GameObject[] stores;
	GameObject[] stations;

	int timer = 0;

	void Start () {
		player =  GameObject.FindGameObjectWithTag("Player");
		playerSprite = map.transform.FindChild("GUIPlayer").gameObject;
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
		GameObject[] tempStores = GameObject.FindGameObjectsWithTag("store");
		GameObject[] tempStations = GameObject.FindGameObjectsWithTag("abdstation");
		stores = new GameObject[tempStores.Length];
		stations = new GameObject[tempStations.Length];
		for(int i=0;i<tempStores.Length;i++){
			GameObject store = tempStores[i];
			Vector3 position = new Vector3(store.transform.position.x/(mapWidth*2)*aspect + 0.5f,store.transform.position.y/(mapWidth*2) + 0.5f,0.9f);
			GameObject newStore = (GameObject)Instantiate(storeSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
			newStore.transform.parent = map.transform;
			newStore.transform.localPosition = position;
			newStore.transform.localScale =  new Vector3(0.1f*aspect,0.1f,0);
			stores[i] = newStore;
		}
		for(int i=0;i<tempStations.Length;i++){
			GameObject station = tempStations[i];
			Vector3 position = new Vector3(station.transform.position.x/(mapWidth*2)*aspect + 0.5f,station.transform.position.y/(mapWidth*2) + 0.5f,0.9f);
			GameObject newStation = (GameObject)Instantiate(abandonedSprite,new Vector3(0,0,0),new Quaternion(0,0,0,0));
			newStation.transform.parent = map.transform;
			newStation.transform.localPosition = position;
			newStation.transform.localScale =  new Vector3(0.1f*aspect,0.1f,0);
			stations[i] = newStation;
		}
	}
	void startFog(){
		for(int i=0;i<unitsValue;i++){
			float yVal = Camera.main.camera.orthographicSize * 2 / unitsValue;
			float xVal = yVal*Camera.main.aspect;
			for(int k=0;k<unitsValue;k++){
				Vector3 position = new Vector3((float)i/unitsValue*aspect + 0.25f,(float)k/unitsValue + 0.5f/unitsValue,11);
				position+=Camera.main.transform.position;
				GameObject newFog = (GameObject)Instantiate(fog,new Vector3(0,0,0),new Quaternion(0,0,0,0));
				newFog.transform.parent = map.transform;
				newFog.transform.localPosition = position;
				newFog.transform.localScale =  new Vector3(1f/unitsValue*aspect,1f/unitsValue,0);
				fogs[i,k] = newFog;
			}
		}
		for(int i=(unitsValue/2)-spawnSize;i<(unitsValue/2)+spawnSize;i++){
			for(int k=(unitsValue/2)-spawnSize;k<(unitsValue/2)+spawnSize;k++){
				Destroy(fogs[i,k]);
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
		Destroy(fogs[xVal,yVal]);
		explored[xVal,yVal] = true;
	}
	void openMap(){
		if(!status){
			status = true;

			Time.timeScale = 0;
			map.SetActive(true);

			playerSprite.transform.position = new Vector3(player.transform.position.x/(mapWidth*2)*aspect + 0.5f,player.transform.position.y/(mapWidth*2) + 0.5f,1);
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
