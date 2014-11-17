using UnityEngine;
using System.Collections;

public class KamikazeEnemyScript : MonoBehaviour {
	
	private GameObject Player;

	public ParticleSystem EnemyExplosion;
	public ParticleSystem Explosion;
	
	public int damage = 0;
	public int movementSpeed = 4;
	public float rotationSpeed = 0.1f;

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
		}
	}
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag.Equals("Player")){
			GameObject p = col.collider.gameObject;
			p.GetComponent<PlayerScript>().receiveDamage(damage);
			p.rigidbody2D.AddForce(Vector3.MoveTowards(this.gameObject.transform.position,p.transform.position,10f));
			ParticleSystem newEnemyExplosion = (ParticleSystem)Instantiate(EnemyExplosion,this.transform.position,new Quaternion(0,0,0,0));
			ParticleSystem newExplosion = (ParticleSystem)Instantiate(Explosion,this.transform.position,new Quaternion(0,0,0,0));
			this.GetComponent<BaseEnemy>().hullDamage(-1);
			Camera.main.GetComponent<Main>().cameraShake(0.5f,0.5f);
			Destroy(this.gameObject);
		}
	}
	void checkPlayer(){
		float distance = Vector3.Distance(this.transform.position,Player.transform.position);
		if(distance < 10){
			seePlayer = true;
		}
	}
	void aim(){
		float retreatMult = 1;
		float distance = Vector3.Distance(this.transform.position,Player.transform.position);
		Vector3 direction;
		direction = this.transform.position - Player.transform.position;
		float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;

		this.GetComponent<BaseEnemy>().setDifference(difference);
		this.GetComponent<BaseEnemy>().setRetreatMult(retreatMult);
	}
}