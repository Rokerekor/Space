using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Convoy : MonoBehaviour {
	public GameObject[] enemyTypes;
	GameObject station;

	int minEnemies = 2;
	int maxEnemies = 3;
	List<GameObject> enemies = new List<GameObject>();

	void Start () {
		spawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void spawnEnemies(){
		int toSpawn = Random.Range(minEnemies,maxEnemies + 1);
		while(enemies.Count < toSpawn){
			int r = Random.Range(0,enemyTypes.Length);
			GameObject newEnemy = (GameObject)Instantiate(enemyTypes[r],Vector3.zero,this.transform.rotation);
			newEnemy.GetComponent<BaseEnemy>().setStation(this.gameObject);
			newEnemy.GetComponent<BaseEnemy>().randomSpawn(this.transform.position,2f);
			newEnemy.GetComponent<BaseEnemy>().setConvoyStatus(true);
			enemies.Add(newEnemy);
		}
	}
	void checkEnemies(){
		if(enemies.Count == 1){
			enemies[0].GetComponent<BaseEnemy>().setStation(station);
			enemies[0].GetComponent<BaseEnemy>().setConvoyStatus(false);
			station.GetComponent<AbandonedStation>().addEnemy(enemies[0]);
			Destroy(this.gameObject);
		}
	}
	public void enemyDead(GameObject destroyed){
		enemies.Remove(destroyed);
		checkEnemies();
	}
	public void setStation(GameObject val){station = val;}
}
