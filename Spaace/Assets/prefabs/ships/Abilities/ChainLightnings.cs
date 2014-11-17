using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChainLightnings : MonoBehaviour {
	public GameObject lightningArc;
	public int key = 0;
	public float cooldown = 1f;


	bool firing = false;
	float damageMult = 1;
	float cooldownTimer = 0;

	List<GameObject> hit = new List<GameObject>();
	GameObject currentShocked;
	PlayerScript script;
	void Start(){
		script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		this.GetComponent<AbilityIconFunc>().setKey(key);
	}
	void Update () {
		if(firing == true && cooldownTimer <= 0){
			chainShock(GameObject.FindGameObjectsWithTag("Player"));
			firing = false;
			cooldownTimer = cooldown;
			foreach(GameObject go in hit){
				if(go.GetComponent<BaseEnemy>() != null){
					go.GetComponent<BaseEnemy>().applyStun(3);
					go.GetComponent<BaseEnemy>().hullDamage(script.getDamage());
				}else if(go.GetComponent<AsteroidScript>() != null){
					Destroy(go.gameObject);
				}
			}
			hit.Clear();
		}
		cooldownTimer -= Time.deltaTime;
	}

	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.Alpha1){
					if(key == 0){
						firing = true;
					}
				}
				if(e.keyCode == KeyCode.Alpha2){
					if(key == 1){
						firing = true;
					}
				}
				if(e.keyCode == KeyCode.Alpha3){
					if(key == 2){
						firing = true;
					}
				}
			}
		}
	}

	void chainShock(GameObject[] shockedArray){
		LayerMask mask = 1 << 8;
		List<GameObject> nextArray = new List<GameObject>();
		foreach(GameObject shocked in shockedArray){
			Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(shocked.transform.position,3f,mask.value);
			foreach(Collider2D enemy in nearbyEnemies){
				GameObject newArc = (GameObject)Instantiate(lightningArc,shocked.transform.position,new Quaternion(0,0,0,0));
				newArc.GetComponent<LineRenderer>().SetPosition(0,shocked.transform.position);
				newArc.GetComponent<LineRenderer>().SetPosition(1,enemy.transform.position);
				hit.Add(enemy.gameObject);
				shock(enemy.gameObject);
			}
		}
	}

	void shock(GameObject currentShocked){
		LayerMask mask = 1 << 8;
		while(currentShocked != null){
			Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(currentShocked.transform.position,3f,mask.value);
			float distance = Mathf.Infinity;
			Collider2D closest = null;
			foreach(Collider2D enemy in nearbyEnemies){
				float testDistance = Vector3.Distance(currentShocked.transform.position,enemy.transform.position);
				if(testDistance < distance){
					if(!hit.Contains(enemy.gameObject)){
						closest = enemy;
						distance = testDistance;
					}
				}
			}
			if(closest != null){
				GameObject newArc = (GameObject)Instantiate(lightningArc,currentShocked.transform.position,new Quaternion(0,0,0,0));
				newArc.GetComponent<LineRenderer>().SetPosition(0,currentShocked.transform.position);
				newArc.GetComponent<LineRenderer>().SetPosition(1,closest.transform.position);
				hit.Add(closest.gameObject);
				currentShocked = closest.gameObject;
				
			}else{
				currentShocked = null;
			}
		}
	}




}
