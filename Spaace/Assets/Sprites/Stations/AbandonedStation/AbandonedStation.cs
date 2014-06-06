using UnityEngine;
using System.Collections;

public class AbandonedStation : MonoBehaviour {
	public GameObject[] enemyTypes;
	public GameObject dItem;
	public int minEnemies;
	public int maxEnemies;

	int timer = 0;
	GameObject[] enemies;
	bool active = false;
	

	void Start () {
		spawnEnemies();
		minEnemies = 2;
		maxEnemies = 6;
	}
	
	void Update () {
		timer++;
		if(timer >= 5){
			checkActive();
		}
		//checkAlive();
	}
	void checkAlive(){
		if(enemies.Length == 0){
			GameObject item = (GameObject)Instantiate(dItem,this.transform.position,this.transform.rotation);
			Destroy(this);
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
			if(distance < 25){
				active = true;
				setEnemyActive(true);
				setEnemyRetreat(false);
			}
		}
	}
	void spawnEnemies(){
		enemies = new GameObject[Random.Range(minEnemies,maxEnemies + 1)];
		for(int i=0;i<enemies.Length;i++){
			int r = Random.Range(0,enemyTypes.Length);
			Vector3 pos = new Vector3(this.transform.position.x + Random.Range(-5.0f,5.0f),this.transform.position.y + Random.Range(-5.0f,5.0f),0);
			GameObject newEnemy = (GameObject)Instantiate(enemyTypes[r],pos,this.transform.rotation);
			newEnemy.GetComponent<BaseEnemy>().setStation(this.gameObject);
			newEnemy.SetActive(false);
			enemies[i] = newEnemy;
		}
	}
	void setEnemyActive(bool val){
		for(int i=0;i<enemies.Length;i++){
			if(enemies[i] != null){
				enemies[i].SetActive(val);
			}
		}
	}
	void setEnemyRetreat(bool val){
		for(int i=0;i<enemies.Length;i++){
			if(enemies[i] != null){
				enemies[i].GetComponent<BaseEnemy>().setRetreat(val);
			}
		}
	}
	public void enemyDead(GameObject destroyed){
		GameObject[] temp = new GameObject[enemies.Length - 1];
		int remove = enemies.Length;
		for(int i=0;i<enemies.Length;i++){
			if(enemies[i] == destroyed){
				remove = i;
			}
			if(i < remove){
				temp[i] = enemies[i];
			}else if(i > remove){
				temp[i-1] = enemies[i];
			}
		}
		enemies = temp;
		checkAlive();
	}
}
