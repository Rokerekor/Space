using UnityEngine;
using System.Collections;

public class BulletDroneScript : MonoBehaviour {
	public int shotSpeed;
	public GameObject bullet;
	GameObject Player;
	PlayerScript pScript;

	int shotCounter = 0;
	bool shooting = false;
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		pScript = Player.GetComponent<PlayerScript>();
	}
	
	void Update () {
		shoot ();
		//transform.rotation = Player.transform.rotation;
		Vector3 mousePosScreen = Input.mousePosition;
		Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
		Vector3 mousePos = new Vector3(mousePosWorld.x, mousePosWorld.y, this.transform.position.z);
		Vector3 direction = this.transform.position - mousePos;
		this.transform.eulerAngles = new Vector3(0,0,Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z);

		shooting = pScript.isShooting();

	}
	void shoot(){
		shotCounter--;
		if(shooting){
			if(shotCounter <= 0){
				GameObject newBullet = (GameObject)Instantiate(bullet,transform.position,transform.rotation);
				Vector3 mousePosScreen = Input.mousePosition;
				Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
				Vector3 mousePos = new Vector3(mousePosWorld.x, mousePosWorld.y, this.transform.position.z);
				Vector3 direction = this.transform.position - mousePos;
				newBullet.transform.eulerAngles = new Vector3(0,0,Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z);
				newBullet.GetComponent<BulletScript>().setSpeed(pScript.getBulletSpeed());
				newBullet.GetComponent<BulletScript>().setDamage(pScript.getDamage());
				shotCounter = shotSpeed;
			}
		}
	}
}
