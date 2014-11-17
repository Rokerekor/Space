using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
	public AudioClip missileExplosionSound;
	public ParticleSystem explosion;

	float delay = 3;
	float damage = 0;

	bool hostile = false;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		delay-=Time.deltaTime;
		if(delay <= 0){
			explode();
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(!hostile){
			if(collider.tag.Equals("Enemy")){
				collider.GetComponent<BaseEnemy>().hullDamage(damage);
				explode();
			}else if(collider.tag.Equals("Asteroid")){
				explode();
			}
		}
		if(hostile){
			if(collider.tag.Equals("Player")){
				collider.GetComponent<PlayerScript>().receiveDamage(damage);
				explode();
			}
		}
	}
	void explode(){
		ParticleSystem newExplosion = (ParticleSystem)Instantiate(explosion,this.transform.position,new Quaternion(0,0,0,0));
		Camera.main.audio.PlayOneShot(missileExplosionSound);
		if(this.renderer.isVisible){
			Camera.main.GetComponent<Main>().cameraShake(0.15f,0.8f);
		}
		Destroy(this.gameObject);
	}
	public void setDelay(float val){delay = val;}
}
