using UnityEngine;
using System.Collections;
using System;

public class Turret : MonoBehaviour {
	public int fireRate = 80;
	public GameObject bullet;
	float rotationSpeed = 7f;
	bool angleReady = false;
	int timer = 0;
	bool active = false;
	GameObject target;
	float fireCounter;

	void Start () {
		fireCounter = fireRate;
	}

	void Update () {
		timer++;
		if(timer > 10){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			float distance = Vector3.Distance(player.transform.position,this.transform.position);
			active = distance < 20;
			timer = 0;
		}
		if(active){
			fireCounter-=Time.deltaTime*60;
			if(target != null){
				aim();
			}
		}
	}
	void aim(){
		Vector3 vel = new Vector3(target.rigidbody2D.velocity.x,target.rigidbody2D.velocity.y,0);
		float distance = Vector3.Distance(this.transform.position,target.transform.position);
		Vector3 direction = this.transform.position - (target.transform.position + ((vel/10)*distance/5f*(Time.deltaTime*60)));
		float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
		if((difference > 0 && difference < 180) || difference < -180){
			this.transform.Rotate(0,0,-rotationSpeed*(Time.deltaTime*60));
		}else{
			this.transform.Rotate(0,0,rotationSpeed*(Time.deltaTime*60));
		}
		if((difference <= 7 && difference >= -7)){
			this.transform.eulerAngles = new Vector3(0,0,Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z);
			fireWeapon();
		}
	}
	void OnTriggerStay2D(Collider2D collider) {
		if(active){
			if(collider.tag.Equals("Enemy") || collider.tag.Equals("Asteroid")){
				target = collider.gameObject;
			}
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if(active){
			if(collider.tag.Equals("Enemy") || collider.tag.Equals("Asteroid")){
				if(collider.gameObject == target){
					target = null;
				}
			}
		}
	}
	void fireWeapon(){
		if(fireCounter < 0){
			GameObject newBullet = (GameObject)Instantiate(bullet,transform.position,transform.rotation);
			newBullet.GetComponent<BulletScript>().setSpeed(30);
			newBullet.transform.Rotate(0,0,0);
			fireCounter = fireRate;
		}
	}
}
