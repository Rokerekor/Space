using UnityEngine;
using System.Collections;

public class BlinkMine : MonoBehaviour {
	public GameObject mine;

	public int key = -1;
	public float cooldown;
	
	float cooldownCounter = 0;
	bool activated = false;
	void Start () {
		this.GetComponent<AbilityIconFunc>().setKey(key);
	}

	void Update () {
		cooldownCounter -= Time.deltaTime;
		if(activated){
			if(cooldownCounter <= 0){
				activate();
				cooldownCounter = cooldown;
			}else{
				activated = false;
			}
		}
	}
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.Alpha1){
					if(key == 0){
						activated = true;
					}
				}
				else if(e.keyCode == KeyCode.Alpha2){
					if(key == 1){
						activated = true;
					}
				}
				else if(e.keyCode == KeyCode.Alpha3){
					if(key == 2){
						activated = true;
					}
				}else if(e.keyCode == KeyCode.Alpha4){
					if(key == 3){
						activated = true;
					}
				}else if(e.keyCode == KeyCode.Alpha4){
					if(key == 3){
						activated = true;
					}
				}
			}
		}
	}
	void activate(){
		GameObject newMine = (GameObject)Instantiate(mine,GameObject.FindGameObjectWithTag("Crosshair").transform.position,transform.rotation);
		activated = false;
	}
}
