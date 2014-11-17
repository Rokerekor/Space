using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public ParticleSystem explosion;
	GameObject target;
	public int straightTimer = 25;
	public float damage = 10;
	public float explosionForce = 1.5f;

	public AudioClip missileExplosionSound;

	float	life = 200;
	public float maxVel = 12f;
	float vel = 4f;
	float accel = 0.07f;
	float maxRot = 6f;
	float rotAccel = 2.5f;
	float rotVel = 0f;
	float explosionRadius = 1f;
	float detectionRadius = 8f;

	bool hostile = false;
	void Start () {

	}
	
	void Update () {
		lifeTime();
		move();
		rotate();
		getTarget();
	}
	void lifeTime(){
		life -= Time.deltaTime*60;
		if(life<0){
			explode(null);
		}
	}
	void getTarget(){
		if(hostile){
			target = GameObject.FindGameObjectWithTag("Player");
		}else{
			if(GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Targeting>().target != null){
				target = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Targeting>().target;
			}else{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position,detectionRadius);
				float distance = -1;
				for(int i=0;i<colliders.Length;i++){
					if(colliders[i].tag == "Enemy" || colliders[i].tag == "Asteroid"){
						float testDistance = Vector3.Distance(colliders[i].transform.position,this.transform.position);
						if(distance == -1 || testDistance < distance){
							distance = testDistance;
							target = colliders[i].gameObject;
						}
					}
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(!hostile){
			if(collider.tag.Equals("Enemy")){
				//collider.GetComponent<BaseEnemy>().hullDamage(damage);
				explode(collider);
			}else if(collider.tag.Equals("Asteroid")){
				explode(collider);
			}
		}
		if(hostile){
			if(collider.tag.Equals("Player")){
				collider.GetComponent<PlayerScript>().receiveDamage(damage);
				explode(collider);
			}
		}
	}
	void explode(Collider2D collider){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position,explosionRadius);
		foreach(Collider2D c in colliders){
			float distance = Vector3.Distance(c.transform.position,transform.position);
			if(!hostile){
				BaseEnemy script = c.GetComponent<BaseEnemy>();
				if(script != null){
					if(distance < explosionRadius){
						script.hullDamage(damage*((explosionRadius-distance)/explosionRadius));
					}
				}
			}
			if(c.rigidbody2D != null){
				float force = explosionForce*((explosionRadius-distance)/explosionRadius);
				c.rigidbody2D.AddForce(force*(this.transform.position - c.transform.position));
			}
		}
		ParticleSystem newExplosion = (ParticleSystem)Instantiate(explosion,this.transform.position,new Quaternion(0,0,0,0));
		Camera.main.audio.PlayOneShot(missileExplosionSound);
		if(this.renderer.isVisible){
			Camera.main.GetComponent<Main>().cameraShake(0.15f,0.8f);
		}
		Destroy(this.gameObject);
	}
	void move(){
		vel+=accel;
		if(vel > maxVel){
			vel = maxVel*Time.deltaTime*60;
		}
		this.transform.rigidbody2D.velocity = transform.up*vel;
	}
	void rotate(){
		if(straightTimer > 0 || target == null){
			straightTimer--;
		}else{
			Vector3 direction = this.transform.position - target.transform.position;
			float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
			if((difference > 0 && difference < 180) || difference < -180){
				rotVel-=rotAccel*Time.deltaTime*60;
			}else{
				rotVel+=rotAccel*Time.deltaTime*60;
			}
			if(rotVel > maxRot){
				rotVel = maxRot;
			}else if(rotVel < -maxRot){
				rotVel = -maxRot;
			}
			transform.Rotate(0,0,rotVel*Time.deltaTime*60);
		}
	}
	public void setTarget(GameObject t){
		target = t;
	}
	public void cancelStraight(){
		straightTimer = 0;
	}
	public void setHostile(bool val){hostile = val;}
	public void setMaxVel(float val){maxVel = val;}
	public void setMaxRot(float val){maxRot = val;}
	public float getDamage(){return damage;}
	public void setDamage(float val){damage = val;}

	public bool getHostile(){return hostile;}
}
