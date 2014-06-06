using UnityEngine;
using System.Collections;

public class LaserDroneScript : MonoBehaviour {

	public int shotSpeed;
	public GameObject bullet;
	GameObject Player;
	PlayerScript pScript;

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
		pScript = Player.GetComponent<PlayerScript>();
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
			GameObject newBullet = (GameObject)Instantiate(bullet,transform.position,transform.rotation);
			newBullet.GetComponent<BulletScript>().setSpeed(pScript.getBulletSpeed());
			newBullet.GetComponent<BulletScript>().setDamage(pScript.getDamage());
			newBullet.transform.Rotate(0,0,0);
			fireCounter = fireRate;
		}
	}
}