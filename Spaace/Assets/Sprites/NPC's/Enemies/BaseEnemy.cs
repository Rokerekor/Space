using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	public float movementSpeed = 4;

	public ParticleSystem EnemyExplosion;
	public ParticleSystem Explosion;

	public float rotationSpeed = 0.1f;
	public float hull = 100;
	float slowMult = 1;
	int slowTime = 0;
	bool stunned = false;
	bool active = true;
	bool retreat = false;
	int stunTime = 0;
	float distanceMult = 1;
	float retreatMult = 1;
	float difference = 0;
	GameObject station;

	void Start () {
	
	}
	void Update () {
		if(!stunned && active){
			move();
			rotate();
			checkStation();
		}
		if(slowTime > 0){
			slowTime--;
		}else{
			slowMult = 1;
		}
		if(stunTime > 0){
			stunTime--;
		}else{
			stunned = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {

	}
	void rotate(){
		if(retreat){
			Vector3 direction = this.transform.position - (station.transform.position + new Vector3(Random.Range(-5.0f,5.0f),Random.Range(-5.0f,5.0f),0));
			difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
		}
		if((difference > 0 && difference < 180) || difference < -180){
			this.rigidbody2D.AddTorque(-rotationSpeed*retreatMult*(Time.deltaTime*60));
		}else{
			this.rigidbody2D.AddTorque(rotationSpeed*retreatMult*(Time.deltaTime*60));
		}
	}
	void move(){
		this.rigidbody2D.AddForce(this.transform.up*movementSpeed*distanceMult*slowMult*(Time.deltaTime*60));
	}
	void checkHull(float damage){
		if(damage == -1){
			hull = 0;
		}else{
			hull -= damage;
		}
		if(hull <= 0){
			Destroy(this.gameObject);
			ParticleSystem newEnemyExplosion = (ParticleSystem)Instantiate(EnemyExplosion,this.transform.position,new Quaternion(0,0,0,0));

			if(station != null){
				station.GetComponent<AbandonedStation>().enemyDead(this.gameObject);
			}
		}
		ParticleSystem newExplosion = (ParticleSystem)Instantiate(Explosion,this.transform.position,new Quaternion(0,0,0,0));
	}
	void checkStation(){
		if(station != null){
			if(Vector3.Distance(this.transform.position,station.transform.position) > 10){
				retreat = true;
			}
		}
	}
	public void applySlow(float slow,int time){
		slowMult = slow;
		slowTime = time;
	}
	public void applyStun(int time){
		stunTime = time;
		stunned = true;
	}
	public void hullDamage(float val){checkHull(val);}
	public void setDifference(float val){difference = val;}
	public void setDistanceMult(float val){distanceMult = val;}
	public void setRetreatMult(float val){retreatMult = val;}
	public void setStation(GameObject val){station = val;}
	public void setRetreat(bool val){retreat = val;}
	//RETURN VALS
	public float hullRemaining(){return hull;}


	public bool isImpaired(){
		if(active && !stunned){
			return false;
		}else{
			return true;
		}
	}
}
