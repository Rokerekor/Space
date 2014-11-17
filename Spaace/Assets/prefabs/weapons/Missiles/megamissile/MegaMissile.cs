using UnityEngine;
using System.Collections;

public class MegaMissile : MonoBehaviour {
	public GameObject missile;

	float speed = 0;
	float damage = 0;
	void Start () {
		
	}
	
	void Update () {
		checkTarget();
		move();
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag.Equals("Enemy")){
			Destroy(this.gameObject);
		}
	}
	void move(){
		rigidbody2D.velocity = transform.up*speed;
	}
	void checkTarget(){
		if(GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Targeting>().target != null){
			GameObject target = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Targeting>().target;
			Vector3 targetPos = target.transform.position;
			float distance = (this.transform.position - targetPos).magnitude;
			if(distance < 1){
				explode();
			}
		}
	}
	public void explode(){
		GameObject newMissile = (GameObject)Instantiate(missile,transform.position + transform.right*0.075f,transform.rotation);
		newMissile.GetComponent<Missile>().cancelStraight();
		newMissile.transform.rigidbody2D.velocity = transform.rigidbody2D.velocity;

		newMissile = (GameObject)Instantiate(missile,transform.position - transform.right*0.075f,transform.rotation);
		newMissile.GetComponent<Missile>().cancelStraight();
		newMissile.transform.rigidbody2D.velocity = transform.rigidbody2D.velocity;

		newMissile = (GameObject)Instantiate(missile,transform.position + transform.right*0.025f,transform.rotation);
		newMissile.GetComponent<Missile>().cancelStraight();
		newMissile.transform.rigidbody2D.velocity = transform.rigidbody2D.velocity;

		newMissile = (GameObject)Instantiate(missile,transform.position - transform.right*0.025f,transform.rotation);
		newMissile.GetComponent<Missile>().cancelStraight();
		newMissile.transform.rigidbody2D.velocity = transform.rigidbody2D.velocity;
		Destroy(this.gameObject);
	}
	public void setDamage(float val){damage = val;}
	public void setSpeed(float val,Vector2 initVel){
		speed = val;
		//rigidbody2D.velocity = transform.up*val;
		//rigidbody2D.velocity += initVel;
	}
	public void setSpeed(float val){speed = val;}
}

