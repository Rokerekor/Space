using UnityEngine;
using System.Collections;

public class SwordfishAbilities : MonoBehaviour {
	public GameObject megaMissile;
	public GameObject missile;
	public GameObject crosshair;
	public GameObject laser;
	GameObject currentMegaMissile;

	bool 		left = false;
	bool 		right = false;

	bool 		abilityOne = false;
	int 		abilityOneCounter = 0;
	int 		abilityOneDuration = 15;
	int 		abilityOneBoost = 10;
	Vector3 	abilityOneDir = Vector3.zero;
	
	bool		abilityTwo = false;
	int 		abilityTwoCounter = 15;
	int 		abilityTwoDuration = 15;
	int 		abilityTwoBoost = 10;
	
	bool		abilityThree = false;
	int 		abilityThreeCounter = 40;
	int 		abilityThreeCountdown = 40;

	bool		abilityFour = false;
	bool		abilityFourFired = false;



	void Start () {
	
	}
	
	void Update () {
		abilityFunctions();
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
				if(e.keyCode == KeyCode.A){
					left = true;
				}
				if(e.keyCode == KeyCode.D){
					right = true;
				}
			}else if(e.type == EventType.keyUp){
				if(e.keyCode == KeyCode.A){
					left = false;
				}
				if(e.keyCode == KeyCode.D){
					right = false;
				}
			}
		}
	}
	void useAbility(int ability){
		if(ability == 1){
			abilityOne = true;
		}else if(ability == 2){
			abilityTwo = true;
		}else if(ability == 3){
			abilityThree = true;
		}else if(ability == 4){
			abilityFour = true;
		}
	}
	void abilityFunctions(){
		if(abilityOne){
//			if(left && abilityOneDir == Vector3.zero){
//				abilityOneDir = this.transform.right*-1;
//				abilityOneCounter = abilityOneDuration;
//			}else if(right && abilityOneDir == Vector3.zero){
//				abilityOneDir = this.transform.right;
//				abilityOneCounter = abilityOneDuration;
//			}else if(abilityOneCounter > 0){
//				abilityOneCounter--;
//				this.transform.rigidbody2D.AddForce(abilityOneDir*abilityOneBoost);
//			}else if(abilityOneDir != Vector3.zero){
//				abilityOneDir = Vector3.zero;
//				abilityOne = false;
//			}

			if(abilityOneDir == Vector3.zero){
				Vector3 mousePosScreen = Input.mousePosition;
				Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
				Vector3 mousePos = new Vector3(mousePosWorld.x, mousePosWorld.y, this.transform.position.z);
				abilityOneDir = this.transform.position - mousePos;
				abilityOneCounter = abilityOneDuration;
			}
			if(abilityOneCounter > 0){
				abilityOneCounter--;
				this.transform.rigidbody2D.AddForce(-abilityOneDir*abilityOneBoost);
			}else if(abilityOneDir != Vector3.zero){
				abilityOneDir = Vector3.zero;
				abilityOne = false;
			}
		}else if(abilityTwo){
//			if(left && abilityOneDir == Vector3.zero){
//				abilityOneDir = this.transform.right*-1;
//				abilityOneCounter = abilityOneDuration;
//			}else if(right && abilityOneDir == Vector3.zero){
//				abilityOneDir = this.transform.right;
//				abilityOneCounter = abilityOneDuration;
//			}

			
		}else if(abilityThree){
			Debug.Log(abilityThreeCounter);
			if(crosshair.GetComponent<Targeting>().isLocked()){
				if(abilityThreeCounter > 0){
					abilityThreeCounter--;
				}else if(abilityThreeCounter > -abilityThreeCountdown*2){
					GameObject target = crosshair.GetComponent<Targeting>().target;
					crosshair.GetComponent<Targeting>().charged(true);
					if(target != null){
						Vector3 direction = this.transform.position - (target.transform.position + new Vector3(target.rigidbody2D.velocity.x,target.rigidbody2D.velocity.y,0)/6);
						float difference = transform.rotation.eulerAngles.z - Quaternion.LookRotation(transform.forward, - direction).eulerAngles.z;
						if(Mathf.Abs(difference) < 2){
							GameObject newLaser = (GameObject)Instantiate(laser,transform.position,transform.rotation);
							newLaser.GetComponent<BulletScript>().setSpeed(20);
							abilityThreeCounter = abilityThreeCountdown;
							abilityThree = false;
							crosshair.GetComponent<Targeting>().charged(false);
						}
					}
					abilityThreeCounter--;
				}else{
					abilityThreeCounter = abilityThreeCountdown;
					abilityThree = false;
					GameObject target = crosshair.GetComponent<Targeting>().target;
					crosshair.GetComponent<Targeting>().charged(false);
				}
			}else{
				abilityThreeCounter = abilityThreeCountdown;
				abilityThree = false;
				GameObject target = crosshair.GetComponent<Targeting>().target;
				crosshair.GetComponent<Targeting>().charged(false);
			}
			
		}else if(abilityFour){
			if(abilityFourFired){
				if(currentMegaMissile != null){
					currentMegaMissile.GetComponent<MegaMissile>().explode();
					abilityFourFired = false;
					abilityFour = false;
				}else{
					abilityFourFired = false;
					abilityFour = false;
				}
			}else if(crosshair.GetComponent<Targeting>().isLocked()){
				currentMegaMissile = (GameObject)Instantiate(megaMissile,transform.position,transform.rotation);
				currentMegaMissile.GetComponent<MegaMissile>().setSpeed(4);
				abilityFour = false;
				abilityFourFired = true;
			}else{
				abilityFour = false;
			}
		}
	}


	void fireMissile(int side){
		int mult = 1;
		if(side%2==1) {
			mult = -1;
		}
		GameObject newMissile = (GameObject)Instantiate(missile,transform.position + transform.right*mult/8,transform.rotation);
		newMissile.transform.rigidbody2D.velocity = transform.rigidbody2D.velocity;
		newMissile.GetComponent<Missile>().setTarget(crosshair.GetComponent<Targeting>().target);
	}
}
