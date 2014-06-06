using UnityEngine;
using System.Collections;

public class RocketEnemyScript : MonoBehaviour {
	
	private GameObject Player;
	public GameObject missile;
	
	public ParticleSystem EnemyExplosion;
	public ParticleSystem Explosion;
	
	public int fireRate = 100;
	public float attackDamage = 5;
	public float bulletForce = 0f;
	public float maxMissileSpeed = 10;
	public float maxMissileRotSpeed = 2;
	public int movementSpeed = 4;
	public float rotationSpeed = 0.1f;
	
	bool 	shooting = 	false;
	int 	state = 0;
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
		if(distance > 8){
			state = 0;
		}else if(distance < 3.5){
			state = 2;
		}else{
			state = 1;
		}
	}
	void aim(){
		float retreatMult = 1;
		float 	distanceMult = 1;
		float distance = Vector3.Distance(this.transform.position,Player.transform.position);
		Vector3 direction;
		direction = this.transform.position - Player.transform.position;
		float angle = Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
		if(state == 1){
			angle-=90;
		}else if(state == 2){
			angle += 180;
		}
		if(angle > 360){
			angle -= 360;
		}
		if(angle < -360){
			angle += 360;
		}
		float difference = transform.rotation.eulerAngles.z - angle;
		if(state == 2){
			retreatMult = Mathf.Abs(difference/5);
			if(retreatMult > 2.5f){
				retreatMult = 2.5f;
			}else if(retreatMult < 0.1f){
				retreatMult = 0.01f;
			}
			if(Mathf.Abs(difference) < 10){
				distanceMult += 0.05f;
				if(distanceMult > 3){
					distanceMult = 3;
				}
			}else{
				distanceMult = 1;
			}
			shooting = false;
		}else	{
			shooting = true;
		}
		this.GetComponent<BaseEnemy>().setDifference(difference);
		this.GetComponent<BaseEnemy>().setRetreatMult(retreatMult);
		this.GetComponent<BaseEnemy>().setDistanceMult(distanceMult);
	}
	void shoot(){
		shotCounter--;
		if(shooting){
			if(shotCounter <= 0){
				float distance = Vector3.Distance(this.transform.position,Player.transform.position);
				if(distance < 9){
					GameObject newMissile = (GameObject)Instantiate(missile,transform.position + transform.up/3,transform.rotation);
					newMissile.GetComponent<Missile>().setHostile(true);
					newMissile.GetComponent<Missile>().setMaxVel(maxMissileSpeed);
					newMissile.GetComponent<Missile>().setMaxRot(maxMissileRotSpeed);
					shotCounter = fireRate;
				}
			}
		}
	}
}