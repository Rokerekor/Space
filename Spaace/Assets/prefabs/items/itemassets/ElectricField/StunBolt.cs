using UnityEngine;	
using System.Collections;

public class StunBolt : MonoBehaviour {
	public GameObject lightningArc;
	GameObject player;
	bool charged = true;
	float max = 6f;

	const float damageMult = 0.4f;
	const float stunMult = 0.5f;
	const float rangeMult = 0.5f;
	float charge = 0;


	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
	}
	void FixedUpdate(){
		//Debug.Log("Charge: " + charge);
		if(charge < max){
			charge += Time.deltaTime;
		}else{
			charge = max;
		}
		this.GetComponent<CircleCollider2D>().radius = charge*rangeMult;
		this.transform.position = player.transform.position;
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag.Equals("Enemy")){
			//Create visuals
			GameObject newArc = (GameObject)Instantiate(lightningArc,Vector3.zero,new Quaternion(0,0,0,0));
			newArc.GetComponent<LineRenderer>().SetPosition(0,player.transform.position);
			newArc.GetComponent<LineRenderer>().SetPosition(1,collider.transform.position);

			//damage enemy
			if(collider.GetComponent<BaseEnemy>() != null){
				collider.GetComponent<BaseEnemy>().hullDamage(player.GetComponent<PlayerScript>().getDamage()*damageMult*charge);
				collider.GetComponent<BaseEnemy>().applyStun(charge*stunMult);
			}

			//timer stuff
			charge = 0;
		}
	}
	public void upgrade(){
		max++;
	}
}
