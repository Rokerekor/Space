using UnityEngine;
using System.Collections;

public class BasicEnemyScript : MonoBehaviour {

	private GameObject Player;
	public GameObject bullet;


	public int fireRate = 20;
	public float attackDamage = 5;
	public float bulletForce = 0f;
	public float bulletSpeed = 10;
	
	bool 	shooting = 	false;
	bool 	retreat = false;
	int 	retreatSpeed = 1;
	bool 	seePlayer = false;
	int 	shotCounter = 0;
	GameObject target;


	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		if(!this.GetComponent<BaseEnemy>().isImpaired()){
			checkPlayer();
			aim();
			shoot();
		}
	}
	void checkPlayer(){
		float distance = Vector3.Distance(this.transform.position,Player.transform.position);
		if(distance < 10){
			seePlayer = true;
		}
		if(!retreat){
			if(distance < 2){
				if((Player.rigidbody2D.velocity - this.rigidbody2D.velocity).magnitude > 1){
					retreat = true;
				}else if(distance < 0.5){
					retreat = true;
				}
			}
		}else{
			if(distance > 3.3){
				retreat = false;
			}
		}
	}
	void aim(){
		float distanceMult = 1;
		float retreatMult = 1;
		float distance = Vector3.Distance(this.transform.position,Player.transform.position);
		Vector3 direction;
		direction = this.transform.position - Player.transform.position;
		if(retreat){
			direction =  -direction;
		}
		float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
		if(retreat){
			retreatMult = Mathf.Abs(difference/5);
			if(retreatMult > 2.5f){
				retreatMult = 2.5f;
			}else if(retreatMult < 0.1f){
				retreatMult = 0.01f;
			}
			if(Mathf.Abs(difference) < 10){
				distanceMult += 0.05f;
				if(distanceMult > 2){
					distanceMult = 2;
				}
			}else{
				distanceMult = 1;
			}
		}else{
			if(Mathf.Abs(difference) < 10){
				if(distance < 4){
					distanceMult-=0.01f;
				}else{
					distanceMult += 0.01f;
					if(distanceMult > 2){
						distanceMult = 2;
					}
				}
				if(distanceMult < 0.4f){
					distanceMult = 0.4f;
				}
				if(distance < 6){
					shooting = true;
				}
			}else{
				distanceMult = 1;
				shooting = false;
			}
		}
		this.GetComponent<BaseEnemy>().setDifference(difference);
		this.GetComponent<BaseEnemy>().setRetreatMult(retreatMult);
		this.GetComponent<BaseEnemy>().setDistanceMult(distanceMult);
	}
	void shoot(){
		shotCounter--;
		if(shooting){
			if(shotCounter <= 0){
				GameObject newBullet = (GameObject)Instantiate(bullet,transform.position + this.transform.up/3,transform.rotation);
				newBullet.GetComponent<BulletScript>().setDamage(attackDamage);
				newBullet.GetComponent<BulletScript>().setForce(bulletForce);
				newBullet.GetComponent<BulletScript>().setHostile(true);
				newBullet.GetComponent<BulletScript>().setSpeed(bulletSpeed,this.rigidbody2D.velocity);
				shotCounter = fireRate;
			}
		}
	}
}