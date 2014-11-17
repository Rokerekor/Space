using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
	public GameObject Asteroid;
	public ParticleSystem AsteroidExplosion;

	int timer = 0;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer++;
		if(timer > 30){
			garbageCleanup();
			timer = 0;
		}
	}
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag.Equals("Player") || col.collider.tag.Equals("Asteroid") || col.collider.tag.Equals("Enemy")){
			if(col.collider.rigidbody2D.mass*col.relativeVelocity.magnitude > this.rigidbody2D.mass){
				split();
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag.Equals("Bullet") ||  collider.tag.Equals("Missile")){
			split();
		}
		if(collider.tag.Equals("Laser")){
			die();
		}
	}
	void die(){
		if(this.renderer.isVisible){
			ParticleSystem newAsteroidExplosion = (ParticleSystem)Instantiate(AsteroidExplosion,this.transform.position,new Quaternion(0,0,0,0));
		}
		Destroy(this.gameObject);
	}
	void garbageCleanup(){
		GameObject cam = GameObject.FindGameObjectWithTag("Player");
		if(this.transform.position.x > cam.transform.position.x + 20){
			die();
		}else if(this.transform.position.x < cam.transform.position.x - 20){
			die();
		}else if(this.transform.position.y > cam.transform.position.y + 14){
			die();
		}else if(this.transform.position.y < cam.transform.position.y - 14){
			die();
		}
	}
	void dropScrap(){
		Camera.main.GetComponent<ScrapManager>().dropScrap(this.transform.position,Random.Range(1,3),this.rigidbody2D.velocity);
	}
	void split(){
		if(rigidbody2D.mass > 5f){
			float random1 = Random.value*0.4f+0.3f;
			float asteroidSize1 = this.transform.localScale.x*random1;
			float asteroidSize2 = this.transform.localScale.x*(1-random1);
			float xVal = Random.value*0.2f-0.1f*this.transform.localScale.x;
			float yVal = Random.value*0.2f-0.1f*this.transform.localScale.x;

			GameObject newAsteroid = (GameObject)Instantiate(Asteroid,new Vector3(transform.position.x+xVal,transform.position.y+yVal,0),new Quaternion(0,0,0,0));
			newAsteroid.transform.localScale = new Vector3(asteroidSize1, asteroidSize1, newAsteroid.transform.localScale.y);
			newAsteroid.rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + Random.value-0.5f,rigidbody2D.velocity.y + Random.value-0.5f);
			newAsteroid.rigidbody2D.angularVelocity = Random.value*20-10;
			newAsteroid.rigidbody2D.mass = 4/3*Mathf.PI*(asteroidSize1/2)*(asteroidSize1/2)*(asteroidSize1/2)*2;
			if(newAsteroid.rigidbody2D.mass < 3f){
				Destroy(newAsteroid);
			}
			newAsteroid = (GameObject)Instantiate(Asteroid,new Vector3(transform.position.x-xVal,transform.position.y-yVal,0),new Quaternion(0,0,0,0));
			newAsteroid.transform.localScale = new Vector3(asteroidSize2*(1f-random1), asteroidSize2*(1f-random1), newAsteroid.transform.localScale.y);
			newAsteroid.rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x  + Random.value-0.5f,rigidbody2D.velocity.y  + Random.value-0.5f);
			newAsteroid.rigidbody2D.angularVelocity = Random.value*20-10;
			newAsteroid.rigidbody2D.mass = 4/3*Mathf.PI*(asteroidSize2/2)*(asteroidSize2/2)*(asteroidSize2/2)*2;
			if(newAsteroid.rigidbody2D.mass < 3f){
				Destroy(newAsteroid);
			}
		}
		die();
	}
}
