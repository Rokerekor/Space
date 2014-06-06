using UnityEngine;
using System.Collections;

public class AsteroidManager : MonoBehaviour {
	public GameObject Asteroid1;
	public GameObject Asteroid2;
	public int asteroidCount = 25;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		if(asteroids.Length < asteroidCount){
			GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
			float asteroidSize = Random.Range(1f,5f);
			int random = Random.Range(1,5);
			float x = 0;
			float y = 0;
			if(random == 1){
				x = -15;
				y = Random.Range(-10,10);
			}else if(random == 2){
				x = 15;
				y = Random.Range(-10,10);
			}else if(random == 3){
				x = Random.Range(-18,18);
				y = -12;
			}else if(random == 4){
				x = Random.Range(-18,18);
				y = 12;
			}
			random = Random.Range(1,3);
			GameObject asteroid;
			if(random == 1){
				asteroid = Asteroid1;
			}else{
				asteroid = Asteroid2;
			}
			GameObject newAsteroid = (GameObject)Instantiate(asteroid,new Vector3(cam.transform.position.x + x,cam.transform.position.y + y,0),new Quaternion(0,0,0,0));
			newAsteroid.transform.localScale = new Vector3(asteroidSize, asteroidSize, newAsteroid.transform.localScale.y);
			newAsteroid.rigidbody2D.velocity = new Vector2(Random.Range(-2f,2f),Random.Range(-2f,2f));
			newAsteroid.rigidbody2D.angularVelocity = Random.Range(-20f,20f);
			newAsteroid.rigidbody2D.mass = 4/3*Mathf.PI*(asteroidSize/2)*(asteroidSize/2)*(asteroidSize/2)*2;
		}
	}
}
