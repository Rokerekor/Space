using UnityEngine;
using System.Collections;

public class DefaultShipAbilities : MonoBehaviour {
	public GameObject megaMissile;
	public GameObject missile;
	public GameObject crosshair;
	public GameObject laser;
	GameObject bullet;

	GameObject currentMegaMissile;

	public Texture abilityOneIconTexture;
	public Texture abilityTwoIconTexture;
	public Texture abilityThreeIconTexture;
	public Texture abilityFourIconTexture;

	public int 	a1ReloadTime = 300;
	public int	a1Cooldown	= 30;
	public int	a1MaxRockets = 4;
	public int 	a1BarrageRockets = 4;
	public int	a1DamageMult = 2;

	public float 	a2Duration = 1;
	public float 	a2Boost = 5;
	public int 	a2Cooldown = 300;

	public int 	a3Cooldown = 600;
	public int	a3DamageMult = 10;
	public int 	a3ProjectileSpeed = 20;

	public int 	a4Angle = 5;
	public int	a4FireRate = 5;
	public int	a4WaveCount = 10;
	public int 	a4WaveSize = 6;
	public int 	a4BulletDamage = 5;
	public int	a4Cooldown = 3000;
	public int	a4DamageMult = 1;

	PlayerScript script;

	bool 		a1 = false;
	int 		a1Counter = 0;
	int 		a1Fired = 4;
	int			a1ReloadCounter = 0;
	int			a1CooldownCounter = 0;
	int 		a1RocketsLoaded = 0;
	bool		a1Progress = false;
	
	bool		a2 = false;
	int 		a2Counter = 0;
	int 		a2CooldownCounter = 0;
	
	bool		a3 = false;
	int 		a3Counter = 0;
	int 		a3Countdown = 40;
	int 		a3CooldownCounter = 0;

	bool		a4 = false;
	bool		a4Fired = false;
	bool		a4Progress = false;
	int 		a4WaveTimer = 0;
	int			a4WaveCounter = 0;
	int			a4CooldownCounter = 0;
	int 		a4Inc = 0;




	void Start () {
		bullet = this.gameObject.GetComponent<PlayerScript>().bullet;

		initializeGUI();
		a1RocketsLoaded = a1MaxRockets;
		a1ReloadCounter = a1ReloadTime;

		a4WaveCounter = a4WaveCount;
		script = this.gameObject.GetComponent<PlayerScript>();
	}
	
	void Update () {
		abilityFunctions();
		abilityCounters();
		abilityGUI();
	}

	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.Alpha1){
					useAbility(1);
				}
				if(e.keyCode == KeyCode.Alpha2){
					useAbility(2);
				}
				if(e.keyCode == KeyCode.Alpha3){
					useAbility(3);
				}
				if(e.keyCode == KeyCode.Alpha4){
					useAbility(4);
				}
			}
		}
	}
	void initializeGUI(){
		GameObject iconObject = GameObject.FindGameObjectWithTag("abilityOneIcon");
		GUITexture icon	 = iconObject.transform.Find("image").GetComponent<GUITexture>();
		icon.texture = abilityOneIconTexture;

		iconObject = GameObject.FindGameObjectWithTag("abilityTwoIcon");
		icon = iconObject.transform.Find("image").GetComponent<GUITexture>();
		icon.texture = abilityTwoIconTexture;

		iconObject = GameObject.FindGameObjectWithTag("abilityThreeIcon");
		icon = iconObject.transform.Find("image").GetComponent<GUITexture>();
		icon.texture = abilityThreeIconTexture;

		iconObject = GameObject.FindGameObjectWithTag("abilityFourIcon");
		icon = iconObject.transform.Find("image").GetComponent<GUITexture>();
		icon.texture = abilityFourIconTexture;
	}
	void abilityGUI(){
		GameObject iconObject = GameObject.FindGameObjectWithTag("abilityOneIcon");
		GUIText primary = iconObject.transform.Find("primaryText").GetComponent<GUIText>();
		GUIText secondary = iconObject.transform.Find("secondaryText").GetComponent<GUIText>();
		if(a1RocketsLoaded > 0){
			if(a1CooldownCounter > 0){
				primary.text = "" + Mathf.CeilToInt(a1CooldownCounter/60f);
			}else{
				primary.text = "";
			}
		}else{
			primary.text = "" + Mathf.CeilToInt(a1ReloadCounter/60f);
		}
		secondary.text = ""+a1RocketsLoaded;



		iconObject = GameObject.FindGameObjectWithTag("abilityTwoIcon");
		primary = iconObject.transform.Find("primaryText").GetComponent<GUIText>();
		secondary = iconObject.transform.Find("secondaryText").GetComponent<GUIText>();
		if(a2CooldownCounter > 0){
			primary.text = "" + Mathf.CeilToInt(a2CooldownCounter/60f);
		}else{
			primary.text = "";
		}
		secondary.text = "";

		iconObject = GameObject.FindGameObjectWithTag("abilityThreeIcon");
		primary = iconObject.transform.Find("primaryText").GetComponent<GUIText>();
		secondary = iconObject.transform.Find("secondaryText").GetComponent<GUIText>();
		if(a3CooldownCounter > 0){
			primary.text = "" + Mathf.CeilToInt(a3CooldownCounter/60f);
		}else{
			primary.text = "";
		}
		secondary.text = "";


		iconObject = GameObject.FindGameObjectWithTag("abilityFourIcon");
		primary = iconObject.transform.Find("primaryText").GetComponent<GUIText>();
		secondary = iconObject.transform.Find("secondaryText").GetComponent<GUIText>();
		if(a4CooldownCounter > 0){
			primary.text = "" + Mathf.CeilToInt(a4CooldownCounter/60f);
		}else{
			primary.text = "";
		}
		secondary.text = "";
	}
	void useAbility(int ability){
		if(ability == 1){
			a1 = true;
		}else if(ability == 2){
			a2 = true;
		}else if(ability == 3){
			a3 = true;
		}else if(ability == 4){
			a4 = true;
		}
	}
	void abilityCounters(){
		a1CooldownCounter--;
		a2CooldownCounter--;
		a3CooldownCounter--;
		a4CooldownCounter--;
		if(a1RocketsLoaded < a1MaxRockets){
			a1ReloadCounter--;
		}
		if(a1ReloadCounter <= 0){
			a1RocketsLoaded++;
			a1ReloadCounter = a1ReloadTime;
		}
	}
	void abilityFunctions(){
				if (a1) {
						if (a1CooldownCounter < 0 && a1RocketsLoaded > 0 && !a1Progress) {
								a1Fired = 0;
								a1CooldownCounter = a1Cooldown;
								a1RocketsLoaded--;
								a1Progress = true;
						}
						if (a1Fired < a1BarrageRockets + script.getLockedAndLoaded ()) {
								a1Counter--;
								if (a1Counter <= 0) {
										fireMissile (a1Fired);
										a1Counter = 5;
										a1Fired++;
								}
						} else {
								a1 = false;
								a1Progress = false;
								a1Counter = 0;
						}
				} else if (a2) {
						if (a2CooldownCounter <= 0) {
								a2CooldownCounter = a2Cooldown;
								script.boosting (a2Boost, a2Duration);

						}
						a2 = false;
				} else if (a3) {
						GameObject target = crosshair.GetComponent<Targeting> ().target;
						if (a3CooldownCounter < 0 && target != null) {
								GameObject newLaser = (GameObject)Instantiate (laser, transform.position + this.transform.up / 2.5f, transform.rotation);
								float distance = Vector3.Distance (this.transform.position, target.transform.position);
								Vector3 vel = new Vector3 (target.rigidbody2D.velocity.x, target.rigidbody2D.velocity.y, 0);
								Vector3 direction = this.transform.position - (target.transform.position + ((vel / 10) * distance / 5f * (Time.deltaTime * 60)));
								newLaser.transform.eulerAngles = new Vector3(0, 0, Quaternion.LookRotation (transform.forward, - direction).eulerAngles.z);
								newLaser.GetComponent<BulletScript>().setSpeed(a3ProjectileSpeed);
								newLaser.GetComponent<BulletScript>().setDamage(a3DamageMult * script.getDamage ());
								newLaser.GetComponent<BulletScript>().setIgnoreAsteroids(true);
								newLaser.GetComponent<BulletScript>().setCarryThrough(true);
								newLaser.GetComponent<BulletScript>().setForce (15);
								//newLaser.GetComponent<ParticleSystem>().startRotation = newLaser.transform.rotation.eulerAngles.z;
								a3Counter = a3Countdown;
								crosshair.GetComponent<Targeting>().charged (false);
								a3CooldownCounter = a3Cooldown;
						}
						a3 = false;
						/*if(crosshair.GetComponent<Targeting>().isLocked() && a3CooldownCounter < 0){
				/*if(a3Counter > 0){ //CHARGE UP TIME REMOVED
					a3Counter--;
				}else if(a3Counter > -a3Countdown*2){
					GameObject target = crosshair.GetComponent<Targeting>().target;
					crosshair.GetComponent<Targeting>().charged(true);
					if(target != null){
						Vector3 direction = this.transform.position - (target.transform.position + new Vector3(target.rigidbody2D.velocity.x,target.rigidbody2D.velocity.y,0)/6);
						float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
						if(Mathf.Abs(difference) < 2){
							GameObject newLaser = (GameObject)Instantiate(laser,transform.position + this.transform.up/2.5f,transform.rotation);
							newLaser.GetComponent<BulletScript>().setSpeed(a3ProjectileSpeed);
							newLaser.GetComponent<BulletScript>().setDamage(a3DamageMult*script.getDamage());
							newLaser.GetComponent<BulletScript>().setIgnoreAsteroids(true);
							newLaser.GetComponent<BulletScript>().setForce(15);
							//newLaser.GetComponent<ParticleSystem>().startRotation = newLaser.transform.rotation.eulerAngles.z;
							a3Counter = a3Countdown;
							a3 = false;
							crosshair.GetComponent<Targeting>().charged(false);
							a3CooldownCounter = a3Cooldown;
						}
					}
					a3Counter--;
				}else{
					a3Counter = a3Countdown;
					a3 = false;
					GameObject target = crosshair.GetComponent<Targeting>().target;
					crosshair.GetComponent<Targeting>().charged(false);
				}
			}else{
				a3Counter = a3Countdown;
				a3 = false;
				GameObject target = crosshair.GetComponent<Targeting>().target;
				crosshair.GetComponent<Targeting>().charged(false);
			}*/
				} else if (a4) {
						if (a4CooldownCounter <= 0 && a4Progress == false) {
								a4Progress = true;
								a4WaveCounter = a4WaveCount + script.getLockedAndLoaded ();
					
								//a4Inc = (a4Angle*2)/(a4WaveSize + script.getShotCount());
						} else if (a4Progress) {
								if (a4WaveCounter > 0) {
										if (a4WaveTimer <= 0) {
												int shots = a4WaveSize + script.getShotCount ();
												float maxAng = a4Angle * (shots - 1);
												float ang = -maxAng / 2;
												while (ang <= maxAng/2) {
														GameObject newBullet = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
														newBullet.transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + ang);
														newBullet.transform.position += newBullet.transform.up / 3;
														newBullet.GetComponent<BulletScript> ().setDamage (a4DamageMult * playerDamage ());
														newBullet.GetComponent<BulletScript> ().setForce (3);
														newBullet.GetComponent<BulletScript> ().setSpeed (script.getBulletSpeed (), this.rigidbody2D.velocity);
								
														a4WaveTimer = a4FireRate;
														ang += a4Angle;
												}
												a4WaveCounter--;
												a4WaveTimer = a4FireRate;
										}
										a4WaveTimer--;
								} else {
										a4WaveCounter = a4WaveCount;
										a4 = false;
										a4CooldownCounter = a4Cooldown;
										a4Progress = false;
								}
						} else {
								a4 = false;
						}
						//Jinx ult
						//			if(a4Fired){
						//				if(currentMegaMissile != null){
						//					currentMegaMissile.GetComponent<MegaMissile>().explode();
						//					a4Fired = false;
						//					a4 = false;
						//				}else{
						//					a4Fired = false;
						//					a4 = false;
						//				}
						//			}else if(crosshair.GetComponent<Targeting>().isLocked()){
						//				currentMegaMissile = (GameObject)Instantiate(megaMissile,transform.position,transform.rotation);
						//				currentMegaMissile.GetComponent<MegaMissile>().setSpeed(4);
						//				a4 = false;
						//				a4Fired = true;
						//			}else{
						//				a4 = false;
						//			}
				}
		}
	int playerDamage(){return script.getDamage();}
	void fireMissile(int side){
		int mult = 1;
		if(side%2==1) {
			mult = -1;
		}
		GameObject newMissile = (GameObject)Instantiate(missile,transform.position + transform.right*mult/8,transform.rotation);
		newMissile.transform.rigidbody2D.velocity = transform.rigidbody2D.velocity;
		newMissile.GetComponent<Missile>().setDamage(a1DamageMult*playerDamage());
	} 
}
