using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Shield : MonoBehaviour {
	public GameObject ring;
	public ParticleSystem ShieldBurst;
	public ParticleSystem Electricity;
	List<GameObject> rings = new List<GameObject>();
	const int recharge = 3;

	float timer = 0;
	bool recharging = false;
	bool vulnerable = false;

	void Start () {
		addRing();
	}
	void FixedUpdate(){
		if(recharging){
			timer -= Time.deltaTime;
			if(timer <= 0){
				ringRecharged();
			}
		}
		this.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(!vulnerable){
			if(collider.tag.Equals("Bullet")){
				if(collider.gameObject.GetComponent<BulletScript>().getHostile()){
					ringDepleted();
					Destroy(collider.gameObject);
					ParticleSystem newShieldBurst = (ParticleSystem)Instantiate(ShieldBurst,this.transform.position,new Quaternion(0,0,0,0));
					ParticleSystem newElectricity = (ParticleSystem)Instantiate(Electricity,this.transform.position,new Quaternion(0,0,0,0));
				}
			}
			if(collider.tag.Equals("Missile")){
				if(collider.gameObject.GetComponent<Missile>().getHostile()){
					ringDepleted();
					Destroy(collider.gameObject);
					ParticleSystem newShieldBurst = (ParticleSystem)Instantiate(ShieldBurst,this.transform.position,new Quaternion(0,0,0,0));
					ParticleSystem newElectricity = (ParticleSystem)Instantiate(Electricity,this.transform.position,new Quaternion(0,0,0,0));
				}
			}
		}
	}
	void ringRecharged(){
		for(int i=0;i<rings.Count;i++){
			if(!rings[i].activeInHierarchy){
				rings[i].SetActive(true);
				this.GetComponent<CircleCollider2D>().radius = 0.6f + 0.06f*i;
				if(i == rings.Count - 1){
					recharging = false;
				}
				vulnerable = false;
				break;
			}
		}
		timer = recharge;
	}
	void ringDepleted(){
		for(int i=rings.Count-1;i>=0;i--){
			if(rings[i].activeInHierarchy){
				rings[i].SetActive(false);
				this.GetComponent<CircleCollider2D>().radius = 0.6f + 0.06f*i;
				if(i == 0){
					vulnerable = true;
				}
				break;
			}
		}
		timer = recharge;
		recharging = true;
	}
	public void addRing(){
		GameObject newRing = (GameObject)Instantiate(ring,Vector3.zero,new Quaternion(0,0,0,0));
		newRing.transform.parent = this.gameObject.transform;
		newRing.transform.localPosition = Vector3.zero;
		float p = 1f + 0.2f*rings.Count;
		newRing.transform.localScale = new Vector3(p,p,p);
		this.GetComponent<CircleCollider2D>().radius = 0.6f + 0.06f*rings.Count;
		rings.Add(newRing);
	}
}
