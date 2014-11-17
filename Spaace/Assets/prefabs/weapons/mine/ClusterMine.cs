using UnityEngine;
using System.Collections;

public class ClusterMine : MonoBehaviour {
	public AudioClip missileExplosionSound;
	public ParticleSystem explosion;
	public GameObject mine;

	public float delay = 1.5f;
	public int mines = 5;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		delay-=Time.deltaTime;
		if(delay <= 0){
			explode();
		}
	}
	void explode(){
		ParticleSystem newExplosion = (ParticleSystem)Instantiate(explosion,this.transform.position,new Quaternion(0,0,0,0));
		for(int i=0;i<mines;i++){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			GameObject newMine = (GameObject)Instantiate(mine,this.transform.position,this.transform.rotation);
			Vector3 direction = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),this.transform.position.z);
			newMine.rigidbody2D.AddForce(direction.normalized*Random.Range(5f,15f));
			newMine.GetComponent<Mine>().setDelay(Random.Range(2.5f,3.5f));
		}
		Destroy(this.gameObject);
	}
}
