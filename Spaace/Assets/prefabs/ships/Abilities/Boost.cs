using UnityEngine;
using System.Collections;

public class Boost : MonoBehaviour {
	public int key = -1;
	public float speed;
	public float duration;
	public AudioClip boostSound;
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
				boost ();
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
				}
			}
		}
	}
	void boost(){
		Camera.main.audio.PlayOneShot(boostSound);
		cooldownCounter = cooldown;
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().boosting(speed, duration);
	}
}
