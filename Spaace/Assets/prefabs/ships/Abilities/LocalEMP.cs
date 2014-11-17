using UnityEngine;
using System.Collections;

public class LocalEMP : MonoBehaviour {
	public GameObject EMP;
	public float cooldown = 1f;
	public int key = 0;
	
	
	bool activated = false;
	float damageMult = 1;
	float cooldownTimer = 0;
	
	void Start(){
		this.GetComponent<AbilityIconFunc>().setKey(key);
	}
	
	void Update () {
		if(activated == true && cooldownTimer <= 0){
			activate();
			cooldownTimer = cooldown;
		}else{
			activated = false;
		}
		cooldownTimer -= Time.deltaTime;
	}
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.Alpha1){
					if(key == 0){
						activated = true;
					}
				}else if(e.keyCode == KeyCode.Alpha2){
					if(key == 1){
						activated = true;
					}
				}else if(e.keyCode == KeyCode.Alpha3){
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
	void activate(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject newEMP = (GameObject)Instantiate(EMP,player.transform.position,new Quaternion(0,0,0,0));
		//Camera.main.audio.PlayOneShot(missileLaunchSounds[Random.Range(0,missileLaunchSounds.Length)]);
		//newMissile.transform.rigidbody2D.velocity = GameObject.FindGameObjectWithTag("Player").transform.rigidbody2D.velocity;
		//newMissile.GetComponent<Missile>().setDamage(damageMult*GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().getDamage());
	} 
}
