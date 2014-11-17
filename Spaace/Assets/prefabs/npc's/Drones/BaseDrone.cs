using UnityEngine;
using System.Collections;

public class BaseDrone : MonoBehaviour {

	float smoothTime;
	float distanceVal;
	GameObject Player;
	bool shifting = false;
	Vector3 velocity = Vector3.zero;
	Vector3 shiftTarget;

	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		smoothTime = Random.Range(0.3f,0.7f);
		distanceVal = Random.Range(0.3f,1.2f);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move();
	}
	void move(){
		float distance = Vector3.Distance(this.transform.position,Player.transform.position);
		if(distance > distanceVal && !shifting){
			transform.position = Vector3.SmoothDamp(transform.position, Player.transform.position, ref velocity, smoothTime);
		}else{
			if(!shifting){
				shifting = true;
				shiftTarget = new Vector3(Player.transform.position.x + Random.Range(-0.5f,0.5f),Player.transform.position.y + Random.Range(-0.5f,0.5f),Player.transform.position.z);
			}else{
				if(Vector3.Distance(this.transform.position,shiftTarget) > 0.1 && distance < distanceVal*1.5f){
					transform.position = Vector3.SmoothDamp(transform.position,	shiftTarget, ref velocity, 0.3f);
				}else{
					shifting = false;
				}
			}
		}
	}

}
