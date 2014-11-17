using UnityEngine;
using System.Collections;

public class GravityWell : MonoBehaviour {

	float timer = 3;
	float gravity = 20;
	float explosion = 45;
	void Start () {
	
	}
	

	void Update () {
		pull();
		timer-=Time.deltaTime;
		if(timer <= 0){
			explode();
		}
	}
	void pull(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y),5);
		foreach(Collider2D c in colliders){
			if(c.rigidbody2D != null){
				Vector3 direction = transform.position - c.transform.position;
				float distance = direction.magnitude;
				Vector2 force = new Vector2(direction.x,direction.y).normalized / (distance*distance);
				c.rigidbody2D.AddForce(force*gravity);
			}
		}
	}
	void explode(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y),5);
		foreach(Collider2D c in colliders){
			if(c.rigidbody2D != null){
				Vector3 direction = c.transform.position - transform.position;
				float distance = direction.magnitude;
				Vector2 force = new Vector2(direction.x,direction.y).normalized / (distance*distance);
				c.rigidbody2D.AddForce(force*explosion);
			}
		}
		Destroy(this.gameObject);
	}
}
