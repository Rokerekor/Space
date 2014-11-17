using UnityEngine;
using System.Collections;

public class Scrap : MonoBehaviour {
	public AudioClip pickupSound;
	int worth = 0;
	GameObject player;
	void Start(){
		this.rigidbody2D.AddTorque(Random.Range(-2f,2f));
		this.rigidbody2D.AddForce(new Vector3(Random.Range(-3f,3f),Random.Range(-3f,3f),0));
		player = GameObject.FindGameObjectWithTag("Player");
	}
	void Update(){

		float distance = Vector3.Distance(this.transform.position,player.transform.position);
		if(distance < player.GetComponent<PlayerScript>().getGrabDistance()){
			Vector3 difference = (player.transform.position - this.transform.position);
			this.rigidbody2D.velocity = 5*difference / (distance*distance);
			//this.rigidbody2D.AddForce(10*difference / (distance*distance));
		}

	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag.Equals("Player")){
			collider.GetComponent<PlayerScript>().metalTransaction(worth);
			Camera.main.audio.PlayOneShot(pickupSound);
			Destroy(this.gameObject);
		}
	}

	public void setWorth(int val){
		worth = val;
	}
}
