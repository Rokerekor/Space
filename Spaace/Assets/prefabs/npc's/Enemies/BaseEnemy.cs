using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {
	public float movementSpeed = 4;

	public ParticleSystem EnemyExplosion;
	public ParticleSystem Explosion;
	public ParticleSystem StunEffect;
	public GameObject damageNumbers;

	public float rotationSpeed = 0.1f;
	public float hull = 0;
	public int scrapWorth = 0;

	public GameObject[] gibs;

	public AudioClip[] enemyDeathSounds;

	float slowMult = 1;
	int slowTime = 0;
	bool stunned = false;
	bool active = true;
	bool retreat = false;
	bool relocating = false;
	bool inConvoy = false;

	float stunTime = 0;
	float distanceMult = 1;
	float retreatMult = 1;
	float difference = 0;
	int evade = 0;
	bool reverse = false;
	GameObject station;
	GameObject damageNumber;

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
			stunTime-=Time.deltaTime;
		}else{
			stunned = false;
		}
	}
	void rotate(){
		LayerMask mask = 1 << 8;
		RaycastHit2D middleRay = Physics2D.Raycast(this.transform.position + this.transform.up*0.25f,Vector3.SqrMagnitude(this.rigidbody2D.velocity)*this.transform.up,2f,mask.value);
		RaycastHit2D rightRay = Physics2D.Raycast(this.transform.position + this.transform.up*0.25f,this.transform.up*3 + this.transform.right	,1.5f,mask.value);
		RaycastHit2D leftRay = Physics2D.Raycast(this.transform.position + this.transform.up*0.25f,this.transform.up*3 - this.transform.right,1.5f,mask.value);
		RaycastHit2D wideRightRay = Physics2D.Raycast(this.transform.position + this.transform.up*0.25f,this.transform.up + this.transform.right	,1f,mask.value);
		RaycastHit2D wideLeftRay = Physics2D.Raycast(this.transform.position + this.transform.up*0.25f,this.transform.up - this.transform.right,1f,mask.value);
		RaycastHit2D sideRightRay = Physics2D.Raycast(this.transform.position + this.transform.up*0.25f,this.transform.right,0.5f,mask.value);
		RaycastHit2D sideLeftRay = Physics2D.Raycast(this.transform.position + this.transform.up*0.25f,-this.transform.right,0.5f,mask.value);
		Debug.DrawLine (this.transform.position + this.transform.up*0.2f , (this.transform.position + Vector3.SqrMagnitude(this.rigidbody2D.velocity)*this.transform.up), Color.cyan);
		Debug.DrawLine (this.transform.position + this.transform.up*0.2f , this.transform.position + (this.transform.up + this.transform.right), Color.cyan);
		Debug.DrawLine (this.transform.position + this.transform.up*0.2f , this.transform.position + (this.transform.up - this.transform.right), Color.cyan);
		Debug.DrawLine (this.transform.position + this.transform.up*0.2f , this.transform.position + (this.transform.up*3 + this.transform.right), Color.cyan);
		Debug.DrawLine (this.transform.position + this.transform.up*0.2f , this.transform.position + (this.transform.up*3 - this.transform.right), Color.cyan);
		Debug.DrawLine (this.transform.position + this.transform.up*0.2f , this.transform.position + this.transform.right/2, Color.cyan);
		Debug.DrawLine (this.transform.position + this.transform.up*0.2f , this.transform.position - this.transform.right/2, Color.cyan);

		bool middle = false;
		bool right = false;
		bool left = false;
		bool wideRight = false;
		bool wideLeft = false;
		if(middleRay.collider != null){
			middle = true;
			float distance = Vector3.Distance(middleRay.collider.transform.position,this.transform.position);
			//Debug.Log(distance);
			if(distance < 0.4f){
				reverse = true;
			}
		}else{
			reverse = false;
		}
		if(rightRay.collider != null){
			right = true;
		}
		if(leftRay.collider != null){
			left = true;
		}
		if(wideRightRay.collider != null){
			wideRight = true;
		}
		if(wideLeftRay.collider != null){
			wideLeft = true;
		}
		if(middle){
			if(!right){
				evade = 1;
			}else if(!left){
				evade = -1;
			}
		}
		if(retreat){
			Vector3 direction = this.transform.position - (station.transform.position + new Vector3(Random.Range(-5.0f,5.0f),Random.Range(-5.0f,5.0f),0));
			difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
		}
		if(retreat){
		}
		if(evade == 0){
			if((difference > 0 && difference < 180) || difference < -180){
				this.rigidbody2D.AddTorque(-rotationSpeed*retreatMult*(Time.deltaTime*60));
			}else{
				this.rigidbody2D.AddTorque(rotationSpeed*retreatMult*(Time.deltaTime*60));
			}
		}else{
			if(evade == 1){
				if(!middle && !left){
					evade = 0;
				}else if(!left){
					this.rigidbody2D.AddTorque(-rotationSpeed*retreatMult*(Time.deltaTime*60));
				}else{
					evade = 2;
				}
			}if(evade == 2){
				if(!wideLeft && !left){
					if(Mathf.Abs(difference) < 5){
						evade = 0;
					}
					this.rigidbody2D.AddTorque(rotationSpeed*retreatMult*(Time.deltaTime*60));
				}else{
					this.rigidbody2D.AddTorque(-rotationSpeed*retreatMult*(Time.deltaTime*60));
				}
			}else if(evade == -1){
				if(!middle && !right){
					evade = 0;
				}else if(!right){
					this.rigidbody2D.AddTorque(rotationSpeed*retreatMult*(Time.deltaTime*60));
				}else{
					evade = -2;
				}
			}if(evade == -2){
				if(!wideRight){
					if(Mathf.Abs(difference) < 5){
						evade = 0;
					}
					this.rigidbody2D.AddTorque(-rotationSpeed*retreatMult*(Time.deltaTime*60));
				}else{
					this.rigidbody2D.AddTorque(rotationSpeed*retreatMult*(Time.deltaTime*60));
				}
			}
		}
	}
	void move(){
		if(reverse){
			this.rigidbody2D.AddForce(-this.transform.up*movementSpeed*distanceMult*slowMult*(Time.deltaTime*60));
		}else{
			this.rigidbody2D.AddForce(this.transform.up*movementSpeed*distanceMult*slowMult*(Time.deltaTime*60));
		}
	}
	void checkHull(float damage){
		if(damage == -1){
			hull = 0;
		}else{
			hull -= damage;
			GameObject newDamageNumber = (GameObject)Instantiate(damageNumbers,this.transform.position,new Quaternion(0,0,0,0));
			newDamageNumber.GetComponent<TextMesh>().text = "" + Mathf.RoundToInt(damage);
		}
		if(hull <= 0){
			ParticleSystem newEnemyExplosion = (ParticleSystem)Instantiate(EnemyExplosion,this.transform.position,new Quaternion(0,0,0,0));
			if(inConvoy){
				station.GetComponent<Convoy>().enemyDead(this.gameObject);
			}else if(station != null){
				station.GetComponent<AbandonedStation>().enemyDead(this.gameObject);
			}
			Camera.main.GetComponent<ScrapManager>().dropScrap(this.transform.position,scrapWorth,this.rigidbody2D.velocity);
			foreach(GameObject gib in gibs){
				GameObject newGib = (GameObject)Instantiate(gib,this.transform.position,new Quaternion(0,0,0,0));
				Vector3 direction = new Vector3(Random.Range(-10f,10f),Random.Range(-10f,10f),Random.Range(-10f,10f));
				newGib.rigidbody2D.AddForce(direction);
				newGib.rigidbody2D.AddTorque(Random.Range(-5,5));
			}
			Camera.main.audio.PlayOneShot(enemyDeathSounds[Random.Range(0,enemyDeathSounds.Length)]);
			Destroy(this.gameObject);
		}
		ParticleSystem newExplosion = (ParticleSystem)Instantiate(Explosion,this.transform.position,new Quaternion(0,0,0,0));
	}
	void checkStation(){
		if(station != null && !inConvoy){
			float distance = Vector3.Distance(this.transform.position,station.transform.position);
			if(distance > 10){
				retreat = true;
			}
		}
	}
	public void randomSpawn(Vector3 location,float randomVal){
		this.transform.position = new Vector3(location.x + Random.Range(-randomVal,randomVal),location.y + Random.Range(-randomVal,randomVal),0);
	}

	public void applySlow(float slow,int time){
		slowMult = slow;
		slowTime = time;
	}
	public void applyStun(float time){
		ParticleSystem stunEffect = (ParticleSystem)Instantiate(StunEffect,this.transform.position,new Quaternion(0,0,0,0));
		stunTime = time;
		stunned = true;
	}
	public void hullDamage(float val){checkHull(val);}
	public void setDifference(float val){difference = val;}
	public void setDistanceMult(float val){distanceMult = val;}
	public void setRetreatMult(float val){retreatMult = val;}
	public void setStation(GameObject val){station = val;}
	public void setRetreat(bool val){retreat = val;}
	public void setConvoyStatus(bool val){inConvoy = val;}
	//RETURN VALS
	public float hullRemaining(){return hull;}
	public GameObject getStation(){return station;}

	public bool isImpaired(){
		if(active && !stunned){
			return false;
		}else{
			return true;
		}
	}
}
