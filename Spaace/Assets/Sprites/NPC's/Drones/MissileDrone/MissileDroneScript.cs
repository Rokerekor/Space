using UnityEngine;
using System.Collections;

public class MissileDroneScript : MonoBehaviour {

	public int shotSpeed;
	public GameObject missile;
	GameObject Player;
	int shotCounter = 0;
	bool shooting = false;
	public int fireRate = 80;
	float rotationSpeed = 7f;
	bool angleReady = false;
	GameObject target;
	
	int fireCounter;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		fireCounter = fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		fireCounter--;
		if(target != null){
			aim();
		}
	}
	void aim(){
		Vector3 vel = new Vector3(target.rigidbody2D.velocity.x,target.rigidbody2D.velocity.y,0);
		Vector3 direction = this.transform.position - (target.transform.position + vel/6);
		float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
		if((difference > 0 && difference < 180) || difference < -180){
			this.transform.Rotate(0,0,-rotationSpeed);
		}else{
			this.transform.Rotate(0,0,rotationSpeed);
		}
		if((difference <= 7 && difference >= -7)){
			this.transform.eulerAngles = new Vector3(0,0,Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z);
			fireWeapon();
		}
	}
	void OnTriggerStay2D(Collider2D collider) {
		if(collider.tag.Equals("Enemy")){
			target = collider.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if(collider.tag.Equals("Enemy")){
			if(collider.gameObject == target){
				target = null;
			}
		}
	}
	void fireWeapon(){
		if(fireCounter < 0){
			GameObject newMissile = (GameObject)Instantiate(missile,transform.position,transform.rotation);
			missile.transform.Rotate(0,0,0);
			fireCounter = fireRate;
		}
	}
}