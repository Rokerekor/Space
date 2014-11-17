using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WebLightning : MonoBehaviour {
	public GameObject lightningArc;
	public int key = 0;
	public 	float cooldown = 1f;

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
			shock(GameObject.FindGameObjectsWithTag("Player"));
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
				}else if(e.keyCode == KeyCode.Alpha2){
					if(key == 1){
						firing = true;
					}
				}else if(e.keyCode == KeyCode.Alpha3){
					if(key == 2){
						firing = true;
					}
				}else if(e.keyCode == KeyCode.Alpha4){
					if(key == 3){
						firing = true;
					}
				}
			}
		}
	}

	void shock(GameObject[] shockedArray){
		LayerMask mask = 1 << 8;
		List<GameObject> nextArray = new List<GameObject>();
		foreach(GameObject shocked in shockedArray){
			Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(shocked.transform.position,3f,mask.value);
			foreach(Collider2D enemy in nearbyEnemies){
				if(!hit.Contains(enemy.gameObject)){
					GameObject newArc = (GameObject)Instantiate(lightningArc,shocked.transform.position,new Quaternion(0,0,0,0));
					newArc.GetComponent<LineRenderer>().SetPosition(0,shocked.transform.position);
					newArc.GetComponent<LineRenderer>().SetPosition(1,enemy.transform.position);
					hit.Add(enemy.gameObject);
					nextArray.Add(enemy.gameObject);
				}
			}
		}
		if(nextArray.Count > 0){
			shock(nextArray.ToArray());
		}
	}



}
