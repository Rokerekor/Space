using UnityEngine;	
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public GameObject bullet;
	public GameObject missile;
	public GameObject crosshair;
	public GameObject missileDrone;
	public GameObject laserDrone;
	public GameObject bulletDrone;
	public GameObject[] thrusters;

	public float initialSpeed;
	public float brakeVal;
	public float responsiveVal;
	public float rotationSpeed;
	public int 	initialFireRate;
	public int 	bulletSpeed;
	public int 	initialDamage;
	public int 	initialFuel;
	public int 	initialHull;
	public float metal;
	public float bulletSpread;
	public float collisionDamage;
	//Passive Upgrades
	float		bulletSlow = 1f; 	//cryo bullets 1 = no slow
	float		bulletStun = 0f; 	//ion bullets 0 = no chance of stun
	int 		knockback = 0;		//uranium bullets 0 = no knockback
	int			lockedAndLoaded = 0;//locked and loaded, each increment adds projectiles to abilities

	//base stats
	int			attackDamage = 0;
	float 		playerSpeed = 0;
	int			fireRate = 0;

	//hull
	float 		hull = 0;
	int 		maxHull = 0;

	//fuel
	int			maxFuel = 0;
	int			fuel = 0;
	int 		fuelConsump = 4;
	int 		fuelCounter = 4;

	//counters
	int 		shotCount = 1;
	float 		shotCounter = 100;
	int 		collisionCounter = 0;

	//booleans
	bool 		shooting = false;
	bool	 	accel = false;
	bool 		braking = false;
	bool 		left = false;
	bool 		right = false;
	bool 		focus = false;
	bool		buying = false;
	Store		store;

	//boost vals
	float boostTime = 0;
	float boostSpeed = 0;

	void Start () {
		hull = initialHull;
		maxHull = initialHull;
		playerSpeed = initialSpeed;
		fuel = initialFuel;
		maxFuel = initialFuel;
		fireRate = initialFireRate;
		shotCounter = fireRate;
		attackDamage = initialDamage;
		collisionDamage = 0.001f;
	}
	void Update () {
		Camera.main.orthographicSize = 6 + this.rigidbody2D.velocity.magnitude/6;
		updateRotation();
		updateMovement();
		updateText();
		shoot();
		checkMouse();
		if(accel == true){
			fuelCounter--;
			if(fuelCounter < 0){
				fuel--;
				fuelCounter=4;
			}
		}
	}
	void checkMouse(){
		if (Input.GetMouseButton(0)){
			shooting = true;
		}else{
			shooting = false;
		}
	}
	void updateText(){
		GUIText fuelText = GameObject.Find("FuelText").guiText;
		fuelText.text = (fuel + " Fuel");
		GUIText hullText = GameObject.Find("HullText").guiText;
		hullText.text = (hull + " Hull");
		GUIText metalText = GameObject.Find("CashText").guiText;
		metalText.text = (metal + " Metal");
	}
	void updateRotation(){
		if(focus){
			Vector3 mousePosScreen = Input.mousePosition;
			Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
			Vector3 mousePos = new Vector3(mousePosWorld.x, mousePosWorld.y, this.transform.position.z);
			Vector3 direction = this.transform.position - mousePos;
			float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
			if((difference > 0 && difference < 180) || difference < -180){
				this.rigidbody2D.AddTorque(-rotationSpeed*(Time.deltaTime*60));
			}else{
				this.rigidbody2D.AddTorque(rotationSpeed*(Time.deltaTime*60));
			}
		}else if(left == true && right == false){

			this.rigidbody2D.AddTorque(rotationSpeed*(Time.deltaTime*60));
		}else if(right == true && left == false){
			this.rigidbody2D.AddTorque(-rotationSpeed*(Time.deltaTime*60));
		}
	}
	void updateMovement(){
		if(boostTime > 0){
			accel = true;
			boostTime-=Time.deltaTime;
			for (int i = 0; i < thrusters.Length; i++){
				TrailRenderer Thrust = thrusters[i].GetComponent<TrailRenderer>();
				Thrust.time = boostTime + 0.1f;
			}
		}else{
			for (int i = 0; i < thrusters.Length; i++) {
				TrailRenderer Thrust = thrusters [i].GetComponent<TrailRenderer> ();
				Thrust.time = 0.1f;
			}
			boostSpeed = 1;
			}

		if(accel){
			setThrusters(true);
			this.transform.rigidbody2D.AddForce(this.transform.up*playerSpeed*boostSpeed*(Time.deltaTime*60));
			this.transform.rigidbody2D.AddForce(-this.rigidbody2D.velocity*responsiveVal*(Time.deltaTime*60));
			return;
		}else{
			setThrusters(false);
		}
		if(braking){
			this.transform.rigidbody2D.AddForce(-this.rigidbody2D.velocity*brakeVal*(Time.deltaTime*60));
		}
	}
	void setThrusters(bool set){
		if (set == true) {
			} else {
				for (int i = 0; i < thrusters.Length; i++) {
				TrailRenderer Thrust = thrusters [i].GetComponent<TrailRenderer> ();
				Thrust.time = 0;
			}
		}
	}
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.W || e.keyCode == KeyCode.UpArrow){
					accel = true;	
				}
				if(e.keyCode == KeyCode.A){
					left = true;	
				}
				if(e.keyCode == KeyCode.D){
					right = true;	
				}
				if(e.keyCode == KeyCode.S){
					braking = true;	
				}
				if(e.keyCode == KeyCode.Z){
					buyItem(0);				}
				if(e.keyCode == KeyCode.X){
					buyItem(1);	
				}
				if(e.keyCode == KeyCode.C){
					buyItem(2);	
				}
				if(e.keyCode == KeyCode.V){
					buyItem(3);	
				}
				if(e.keyCode == KeyCode.B){
					buyItem(4);	
				}
			}else if(e.type == EventType.KeyUp){
				if(e.keyCode == KeyCode.W || e.keyCode == KeyCode.UpArrow){
					accel = false;	
				}
				if(e.keyCode == KeyCode.A){
					left = false;	
				}
				if(e.keyCode == KeyCode.D){
					right = false;	
				}
				if(e.keyCode == KeyCode.S){
					braking = false;	
				}
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag.Equals("Enemy") || col.collider.tag.Equals("Asteroid")){
			hull -= (int)(col.relativeVelocity.magnitude*col.relativeVelocity.magnitude*col.collider.rigidbody2D.mass*collisionDamage);
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag.Equals("BuyTrigger")){
			store = collider.gameObject.transform.parent.GetComponent<Store>();
			buying = true;
		}
	}
	void OnTriggerExit2D(Collider2D collider) {
		if(collider.tag.Equals("BuyTrigger")){
			buying = false;
		}
	}
	void OnCollisionStay2D(Collision2D col) {
		if(col.collider.tag.Equals("Enemy") || col.collider.tag.Equals("Asteroid")){
			collisionCounter++;
			if(collisionCounter >= 10){
				hull -= 1;
				collisionCounter = 0;
			}
		}
	}
	void OnCollisionLeave2D(Collision2D col) {
		if(col.collider.tag.Equals("Enemy") || col.collider.tag.Equals("Asteroid")){
			collisionCounter = 0;
		}
	}
	void buyItem(int val){
		if(buying){
			store.buyItem(val);
		}
	}
	void shoot(){
		shotCounter-=Time.deltaTime*60;
		if(shooting){
			if(shotCounter <= 0){
				int count = shotCount;
				float inc = Mathf.FloorToInt(count/2f);
				float shift = 0;
				int dir = 1;
				if(count%2==0){
					shift = 0.05f;
				}
				while(count != 0){
					GameObject newBullet = (GameObject)Instantiate(bullet,transform.position + this.transform.up/5 + transform.right*inc*dir*0.1f - transform.right*shift*dir,transform.rotation);
					Vector3 mousePosScreen = Input.mousePosition;
					Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
					Vector3 mousePos = new Vector3(mousePosWorld.x, mousePosWorld.y, this.transform.position.z);
					Vector3 direction = this.transform.position - mousePos;
					newBullet.transform.eulerAngles = new Vector3(0,0,Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z + Random.Range(-bulletSpread,bulletSpread));

					newBullet.GetComponent<BulletScript>().setDamage(attackDamage);
					newBullet.GetComponent<BulletScript>().setForce(knockback);
					newBullet.GetComponent<BulletScript>().setSlow(bulletSlow);
					newBullet.GetComponent<BulletScript>().setStun(bulletStun);
					newBullet.GetComponent<BulletScript>().setSpeed(bulletSpeed);
					Camera.main.GetComponent<Main>().cameraShake(0.02f,0.2f);
					shotCounter = fireRate;
					count--;
					if(dir > 0){
						dir*=-1;
					}else{
						dir*=-1;
						inc--;
					}
					cameraKick(-direction);
				}
			}
		}
	}
	public void shipKnockback(Vector3 direction, float force){
		this.rigidbody2D.AddForce(direction*force);
	}
	void cameraKick(Vector3 direction){
		//Camera.main.GetComponent<Main>().cameraKick(direction);
	}
	//MISC
	public void repairHull(int val){hull += val;}
	public void refuel(int val){fuel += val;}
	public void boosting(float speed,float time){
		boostSpeed = speed;
		boostTime = time;
	}
	//UPGRADES
	public void upgradeShip(string upgrade){
		if(upgrade.Equals("cryo")){
			bulletSlow = 0.2f;
		}else if(upgrade.Equals("ion")){
			bulletStun = 1f;
		}
	}
	public void addDrone(string type){
		GameObject newDrone;
		if(type.Equals("missile")){
			newDrone = (GameObject)Instantiate(missileDrone,transform.position,transform.rotation);
		}else if(type.Equals("bullet")){
			newDrone = (GameObject)Instantiate(bulletDrone,transform.position,transform.rotation);
		}else if(type.Equals("laser")){
			newDrone = (GameObject)Instantiate(laserDrone,transform.position,transform.rotation);
		}
	}
	//MISC FUNCTIONS
	public void receiveDamage(float val){
		Camera.main.GetComponent<Main>().Glow(.2f);
		hull-=val;
	}
	public void metalTransaction(int val){metal+=val;}
	//STAT UPGRADES
	public void increaseHull(int val){maxHull+=val;}
	public void increaseFuel(int val){maxFuel+=val;}
	public void increaseSpeed(float val){playerSpeed+=val;}
	public void increaseFireRate(int val){fireRate-=val;}
	public void increaseShotCount(int val){shotCount+=val;}
	public void increaseDamage(int val){attackDamage+=val;}
	public void increaseKnockback(int val){knockback+=val;}
	public void increaseLockedAndLoaded(int val){lockedAndLoaded+=val;}
	//GET FUNCTIONS
	public bool isShooting(){return shooting;}
	public int 	getDamage(){return attackDamage;}
	public int 	getBulletSpeed(){return bulletSpeed;}
	public int 	getLockedAndLoaded(){return lockedAndLoaded;}
	public int 	getFireRate(){return fireRate;}
	public int 	getShotCount(){return shotCount;}
}
