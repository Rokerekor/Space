using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public AudioClip[] hitSounds;

	float speed = 0;
	float damage = 0;
	float force = 0;
	float slow = 1;
	float stun = 0;
	float pierce = 0;
	int slowTicks = 100;
	float stunTime = 0.8f;
	bool ignoreAsteroids = false;
	bool carryThrough = false;
	bool hostile = false;
	int timer = 0;

	void Start () {
		//rigidbody2D.velocity = transform.up*speed;
	}
	
	void Update () {
		timer++;
		if(timer > 30){
			garbageCleanup();
			timer = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag.Equals("Enemy")){
			if(!hostile){
				if(slow != 1){
					collider.GetComponent<BaseEnemy>().applySlow(slow,slowTicks);
				}
				stunTarget(collider);
				float damageDealt = damage;
				if(carryThrough){
					damage -= collider.GetComponent<BaseEnemy>().hullRemaining();
				}
				collider.GetComponent<BaseEnemy>().hullDamage(damageDealt);
				hit(collider);
			}
		}
		if(collider.tag.Equals("Asteroid")){
			if(!ignoreAsteroids){
				hit(collider);
			}
		}
		if(collider.tag.Equals("Player")){
			if(hostile){
				collider.GetComponent<PlayerScript>().receiveDamage(damage);
				hit(collider);
			}
		}
	}
	void stunTarget(Collider2D collider){
		float stunLength = 0;
		while(stun > 0){
			if(Random.Range(0,10) < stun){
				stunLength += stunTime;
			}
			stun -= 10;
		}
		if(stunLength > 0){
			collider.GetComponent<BaseEnemy>().applyStun(stunLength);	
		}
	}
	void hit(Collider2D collider){
		Camera.main.audio.PlayOneShot(hitSounds[Random.Range(0,hitSounds.Length)]);
		if(this.GetComponentInChildren<TrailRenderer>() != null){
			if(this.GetComponentInChildren<TrailRenderer>().isVisible){
				Camera.main.GetComponent<Main>().cameraShake(.1f, .5f);
			}
		}else{
			if(this.renderer.isVisible){
				Camera.main.GetComponent<Main>().cameraShake(.1f, .5f);
			}
		}
		collider.rigidbody2D.AddForce(rigidbody2D.velocity*force);
		int randomVal = Random.Range(0,10);
		if(randomVal < pierce){
			pierce -= 10;
		}else if(!carryThrough || damage <= 0){
			Destroy(this.gameObject);
		}
	}
	void garbageCleanup(){
		GameObject cam = GameObject.FindGameObjectWithTag("Player");
		if(this.transform.position.x > cam.transform.position.x + 20){
			Destroy(this.gameObject);
		}else if(this.transform.position.x < cam.transform.position.x - 20){
			Destroy(this.gameObject);
		}else if(this.transform.position.y > cam.transform.position.y + 14){
			Destroy(this.gameObject);
		}else if(this.transform.position.y < cam.transform.position.y - 14){
			Destroy(this.gameObject);
		}
	}
	//SET VARIABLES
	public void setHostile(bool val){hostile = val;}
	public void setIgnoreAsteroids(bool val){ignoreAsteroids = val;}
	public void setCarryThrough(bool val){carryThrough = val;}
	public void setSlow(float val){slow = val;}
	public void setStun(float val){stun = val;}
	public void setDamage(float val){damage = val;}
	public void setForce(float val){force = val;}
	public void setPierce(float val){pierce = val;}
	public void setSpeed(float val,Vector2 initVel){
		rigidbody2D.velocity = transform.up*val;
		rigidbody2D.velocity += initVel;
	}
	public void setSpeed(float val){rigidbody2D.velocity = transform.up*val;}
	//GET VARIABLES
	public float getDamage(){return damage;}
	public bool getHostile(){return hostile;}

}
