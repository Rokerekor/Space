using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbandonedStation : MonoBehaviour {
	public GameObject[] enemyTypes;
	public GameObject messenger;
	public GameObject iManager;
	public int minEnemies;
	public int maxEnemies;

	bool cleared = false;
	int id = -1;
	int timer = 0;
	int sector = 0;

	bool relocating = false;

	List<GameObject> enemies = new List<GameObject>();
	//GameObject[] enemies;
	bool active = false;
	

	void Start () {
		spawnEnemies();
		spawnMessenger();
		//minEnemies = 2;
		//maxEnemies = 6;
	}
	
	void Update () {
		if(!cleared){
			checkActive();
			//checkAlive();
			if(!active){
				checkEnemies();
			}
		}
	}
	void checkEnemies(){
		SectorsManager smScript = GameObject.FindGameObjectWithTag("sectorManager").GetComponent<SectorsManager>();
		int secExplored = smScript.getExploredSector();

		/*if(!relocating && enemies.Count > 2){
			int toRelocate = Random.Range(0,enemies.Count);
			if(enemies[toRelocate].GetComponent<KamikazeEnemyScript>() != null){
				return;
			}
			enemies[toRelocate].GetComponent<BaseEnemy>().relocate();
			enemies.RemoveAt(toRelocate);
			relocating = true;
		}*/
	}
	void checkAlive(){
		if(enemies.Count == 0){
			//Tell sector the station is clear
			GameObject sectorManager = GameObject.FindGameObjectWithTag("sectorManager");
			sectorManager.GetComponent<SectorsManager>().stationClear(sector);

			//Tell player the station is clear
			GameObject gui = GameObject.FindGameObjectWithTag("GUI");
			gui.GetComponentInChildren<centerText>().showText("Station Clear!!!",4);

			//Tell map the station is clear
			Camera.main.GetComponent<Map>().stationClear(this.gameObject);


			//Drop item
			iManager.GetComponent<DroppedItemManager>().dropItem(this.gameObject);

			cleared = true;
		}
	}
	void checkActive(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		float distance = Vector3.Distance(player.transform.position,this.transform.position);
		if(active){
			if(distance > 25){
				active = false;
				setEnemyActive(false);
			}else if(distance > 15){
				setEnemyRetreat(true);
			}else{
				setEnemyRetreat(false);
			}
		}else{
			if(distance < 20){
				/*if(id == -1){
					//Tell player the station is found
					GameObject gui = GameObject.FindGameObjectWithTag("GUI");
					gui.GetComponentInChildren<centerText>().showText("Station nearby, location added to map.",2);
					Camera.main.GetComponent<Map>().stationFound(this.gameObject);
				}*/
				active = true;
				setEnemyActive(true);
				setEnemyRetreat(false);
			}
		}
	}
	void spawnMessenger(){
		GameObject newMessenger = (GameObject)Instantiate(messenger,this.transform.position,new Quaternion(0,0,0,0));
		newMessenger.GetComponent<BaseEnemy>().setStation(this.gameObject);
	}
	void spawnEnemies(){
		int toSpawn = Random.Range(minEnemies,maxEnemies + 1);
		while(enemies.Count < toSpawn){
			int r = Random.Range(0,enemyTypes.Length);
			GameObject newEnemy = (GameObject)Instantiate(enemyTypes[r],Vector3.zero,this.transform.rotation);
			newEnemy.GetComponent<BaseEnemy>().setStation(this.gameObject);
			newEnemy.GetComponent<BaseEnemy>().randomSpawn(this.transform.position,8f);
			newEnemy.SetActive(false);
			enemies.Add(newEnemy);
		}
	}
	void setEnemyActive(bool val){
		for(int i=0;i<enemies.Count;i++){
			if(enemies[i] != null){
				enemies[i].SetActive(val);
			}
		}
	}
	void setEnemyRetreat(bool val){
		for(int i=0;i<enemies.Count;i++){
			if(enemies[i] != null){
				enemies[i].GetComponent<BaseEnemy>().setRetreat(val);
			}
		}
	}
	public void addEnemy(GameObject toAdd){
		enemies.Add(toAdd);
	}
	public void enemyDead(GameObject destroyed){
		enemies.Remove(destroyed);
		checkAlive();
	}
	public void setRelocating(bool val){relocating = val;}
	public int getSector(){return sector;}
	public void setSector(int val){sector = val;}
	public int getID(){return id;}
	public void setID(int val){id = val;}
	public bool isActive(){return active;}
	public bool isCleared(){return cleared;}
}
