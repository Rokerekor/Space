using UnityEngine;
using System.Collections;

public class MissileSalvo : MonoBehaviour {
	public GameObject missile;
	public int missiles = 2;
	public float cooldown = 1f;
	public float delay = 0.3f;
	public int key = 0;


	bool firing = false;
	int fired = 0;
	int side = 1;
	float delayTimer = 0;
	float damageMult = 1;
	float cooldownTimer = 0;
	void Start(){
		this.GetComponent<AbilityIconFunc>().setKey(key);
	}

	void Update () {
		if(firing == true && cooldownTimer <= 0){
			if(delayTimer <= 0){
				fireMissile();
				side*=-1;
				delayTimer = delay;
				fired++;
			}else{
				delayTimer-=Time.deltaTime;
			}
			if(fired >= missiles){
				firing = false;
				fired = 0;
				cooldownTimer = cooldown;
			}
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
	void fireMissile(){
		GameObject newMissile = (GameObject)Instantiate(missile,transform.position + transform.right*side/8,transform.rotation);
		//Camera.main.audio.PlayOneShot(missileLaunchSounds[Random.Range(0,missileLaunchSounds.Length)]);
		newMissile.transform.rigidbody2D.velocity = GameObject.FindGameObjectWithTag("Player").transform.rigidbody2D.velocity;
		newMissile.GetComponent<Missile>().setDamage(damageMult*GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().getDamage());
	} 
}
