using UnityEngine;	
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public AudioClip[] laserSounds;
	public AudioClip engineSound;

	public GameObject bullet;
	public GameObject missile;
	public GameObject missileDrone;
	public GameObject laserDrone;
	public GameObject bulletDrone;
	public GameObject[] mainThrusters;

	public int[] 	speedArray;
	public float 	brakeVal;
	public float 	responsiveVal;
	public float[] 	rotationSpeedArray;
	public int[] 	fireRateArray;
	public int 		bulletSpeed;
	public int[]	damageArray;
	public int[] 	fuelArray;
	public int[] 	hullArray;
	public float 	metal;
	public float 	bulletSpread;
	public float 	collisionDamage;


	GameObject gui;

	//Useitem
	GameObject useItem;

	//Passive Upgrades
	float		bulletSlow = 1f; 	//slow value 1 = no slow | 0 = rooted
	float		bulletStun = 0f; 	//chance to stun 0 = no chance | 10 = always 
	float		bulletPierce = 0f; 	//chance to pierce 0 = no chance | 10 = always
	float 		bulletWidth = 0.2f;
	int 		knockback = 0;		//uranium bullets 0 = no knockback
	int			lockedAndLoaded = 0;//locked and loaded, each increment adds projectiles to abilities

	//base stats
	int			attackDamage = 0;
	float 		playerSpeed = 0;
	int			fireRate = 0;
	float 		grabDistance = 2f;
	float 		rotationSpeed = 0;

	//hull
	float 		hull = 0;
	int 		maxHull = 0;

	//fuel
	int			maxFuel = 0;
	int			fuel = 0;
	int 		fuelConsumpSpeed = 8;
	float 		fuelCounter = 0;

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
		gui = GameObject.FindGameObjectWithTag("GUI");

		fuelCounter = fuelConsumpSpeed;
		maxFuel = fuelArray[0];
		playerSpeed = speedArray[0];
		fuel = fuelArray[0];
		maxHull = hullArray[0];
		hull = maxHull;
		fireRate = fireRateArray[0];
		shotCounter = fireRate;
		attackDamage = damageArray[0];
		rotationSpeed = rotationSpeedArray[0];

		collisionDamage = 0.001f;
	}
	void Update () {
		if(Time.timeScale > 0){
			Camera.main.orthographicSize = 6 + this.rigidbody2D.velocity.magnitude/6;
			updateThrusters();
			updateRotation();
			updateMovement();
			updateText();
			updateLine();
			shoot();
			checkMouse();
			if(accel == true){
				fuelCounter-=Time.deltaTime*60;
				if(fuelCounter < 0){
					fuel--;
					fuelCounter=fuelConsumpSpeed;
				}
			}
		}
	}
	/*void spawnUseItem(){
		GameObject itemUse = (GameObject)Instantiate(useItem,this.transform.position,new Quaternion(0,0,0,0));
	}*/
	void checkMouse(){
		if (Input.GetMouseButton(0)){
			shooting = true;
		}else{
			shooting = false;
		}
	}
	void updateLine(){
		this.GetComponent<LineRenderer>().SetPosition(0,this.transform.position);
		this.GetComponent<LineRenderer>().SetPosition(1,GameObject.FindGameObjectWithTag("Crosshair").transform.position);
	}
	void updateText(){
		gui.transform.Find("fuelText").GetComponent<TextMesh>().text = (fuel + " Fuel");
		gui.transform.Find("hullText").GetComponent<TextMesh>().text = (hull + " Hull");
		gui.transform.Find("metalText").GetComponent<TextMesh>().text = (metal + " Metal");
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
		}else{
			boostSpeed = 1;
			}

		if(accel){
			this.transform.rigidbody2D.AddForce(this.transform.up*playerSpeed*boostSpeed*(Time.deltaTime*60));
			this.transform.rigidbody2D.AddForce(-this.rigidbody2D.velocity*responsiveVal*(Time.deltaTime*60));
			return;
		}
		if(braking){
			this.transform.rigidbody2D.AddForce(-this.rigidbody2D.velocity*brakeVal*(Time.deltaTime*60));
		}
	}
	void updateThrusters(){
		if(accel){
			if(boostTime > 0){
				setMainThrusters(boostTime + 0.1f);
			}else{
				setMainThrusters(0.1f);
			}
		}else{
			setMainThrusters(0f);
		}
		GameObject rightThruster = this.transform.Find("RightThruster").gameObject;
		GameObject leftThruster = this.transform.Find("LeftThruster").gameObject;
		if(right && !left){
			leftThruster.GetComponent<TrailRenderer>().time = 0.1f;
		}else if(left && !right){
			rightThruster.GetComponent<TrailRenderer>().time = 0.1f;
		}else{
			leftThruster.GetComponent<TrailRenderer>().time = 0f;
			rightThruster.GetComponent<TrailRenderer>().time = 0f;
		}
	}
	void setMainThrusters(float val){
		for (int i = 0; i < mainThrusters.Length; i++) {
			if(mainThrusters[i].name.Equals("MainThruster")){
				TrailRenderer Thrust = mainThrusters[i].GetComponent<TrailRenderer> ();
				Thrust.time = val;
			}
		}
	}
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.W || e.keyCode == KeyCode.UpArrow){
					if(accel == false){
						accel = true;
						Camera.main.audio.PlayOneShot(engineSound);
					}
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
				/*if(e.keyCode == KeyCode.Alpha4){
					spawnUseItem();
				}*/
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
			receiveDamage((int)(col.relativeVelocity.magnitude*col.relativeVelocity.magnitude*col.collider.rigidbody2D.mass*collisionDamage));
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
				receiveDamage(1);
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
					Camera.main.audio.PlayOneShot(laserSounds[Random.Range(0,laserSounds.Length)]);

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
					newBullet.GetComponent<BulletScript>().setPierce(bulletPierce);
					newBullet.GetComponent<BoxCollider2D>().size = new Vector2(bulletWidth,2f);
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
	public bool repairHull(int val){
		if(hull == maxHull){
			return false;
		}
		hull += val;
		if(hull > maxHull){
			hull =  maxHull;
		}
		return true;
	}
	
	public bool refuel(int val){
		if(fuel == maxFuel){
			return false;
		}
		fuel += val;
		if(fuel > maxFuel){
			fuel =  maxFuel;
		}
		return true;
	}
	public void boosting(float speed,float time){
		boostSpeed = speed;
		boostTime = time;
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
		if(hull <= 0){
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
	public bool metalTransaction(int val){
		if(metal + val >= 0){
			metal+=val;
			return true;
		}else{
			return false;
		}
	}

	//UseItem
	public void setUseItem(GameObject val){
		useItem = val;
	}
	public bool dockUpgrade(int level,int cost,string upgrade){
		if(cost <= metal){
			if(!upgradeStat(upgrade,level)){
				return false;
			}
			metal-=cost;
			return true;
		}
		return false;
	}
	bool upgradeStat(string upgrade,int level){
		if(upgrade == "damage"){
			if(level < damageArray.Length){
				attackDamage = damageArray[level];
				return true;
			}
		}else if(upgrade == "fireRate"){
			if(level < fireRateArray.Length){
				fireRate = fireRateArray[level];
				return true;
			}
		}else if(upgrade == "hull"){
			if(level < hullArray.Length){
				maxHull = hullArray[level];
				return true;
			}
		}else if(upgrade == "fuel"){
			if(level < fuelArray.Length){
				maxFuel = fuelArray[level];
				return true;
			}
		}else if(upgrade == "agility"){
			if(level < speedArray.Length && level < rotationSpeedArray.Length){
				playerSpeed = speedArray[level];
				rotationSpeed = rotationSpeedArray[level];
				return true;
			}
		}else if(upgrade == "refuel"){
			if(fuel < maxFuel){
				fuel += 10;
				if(fuel > maxFuel){
					fuel = maxFuel;
				}
			}
		}else if(upgrade == "repair"){
			if(fuel < maxFuel){
				fuel += 10;
				if(fuel > maxFuel){
					fuel = maxFuel;
				}
			}
		}
		return false;
	}
	//STAT UPGRADES
	public void increaseSlow(float val){bulletSlow+=val;}
	public void increaseStun(float val){bulletStun+=val;}
	public void increasePierce(float val){bulletPierce+=val;}
	//public void increaseHull(int val){maxHull+=val;hull+=val;}
	//public void increaseFuel(int val){maxFuel+=val;fuel+=val;}
	//public void increaseSpeed(float val){playerSpeed+=val;}
	//public void increaseFireRate(int val){fireRate-=val;}
	public void increaseShotCount(int val){shotCount+=val;}
	//public void increaseDamage(int val){attackDamage+=val;}
	public void increaseKnockback(int val){knockback+=val;}
	public void increaseLockedAndLoaded(int val){lockedAndLoaded+=val;}
	public void increaseBulletWidth(float val){bulletWidth += val;}
	//GET FUNCTIONS
	public bool 	isShooting(){return shooting;}
	public int 		getDamage(){return attackDamage;}
	public int 		getBulletSpeed(){return bulletSpeed;}
	public int 		getLockedAndLoaded(){return lockedAndLoaded;}
	public int 		getFireRate(){return fireRate;}
	public int 		getShotCount(){return shotCount;}
	public float 	getGrabDistance(){return grabDistance;}
}
