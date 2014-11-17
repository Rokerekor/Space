using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {
	public int key = -1;
	public float distance;
	public bool directed = false;
	public AudioClip blinkSound;
	public float cooldown;
	
	float cooldownCounter = 0;
	bool activated = false;
	void Start () {
		this.GetComponent<AbilityIconFunc>().setKey(key);
	}
	
	void Update () {
		cooldownCounter -= Time.deltaTime;
		if(activated){
			if(cooldownCounter <= 0){
				blink();
				cooldownCounter = cooldown;
			}else{
				activated = false;
			}
		}
	}
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.Alpha1){
					if(key == 0){
						activated = true;
					}
				}
				else if(e.keyCode == KeyCode.Alpha2){
					if(key == 1){
						activated = true;
					}
				}
				else if(e.keyCode == KeyCode.Alpha3){
					if(key == 2){
						activated = true;
					}
				}else if(e.keyCode == KeyCode.Alpha4){
					if(key == 3){
						activated = true;
					}
				}else if(e.keyCode == KeyCode.Alpha4){
					if(key == 3){
						activated = true;
					}
				}
			}
		}
	}
	void blink(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(directed){
			GameObject crosshair = GameObject.FindGameObjectWithTag("Crosshair");

			//rotate player
			Vector3 mousePosScreen = Input.mousePosition;
			Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
			Vector3 mousePos = new Vector3(mousePosWorld.x, mousePosWorld.y, player.transform.position.z);
			Vector3 direction = player.transform.position - mousePos;
			player.transform.eulerAngles = new Vector3(0,0,Quaternion.LookRotation(player.transform.forward, - direction).eulerAngles.z);

			//teleport player
			if((player.transform.position - crosshair.transform.position).magnitude > distance){
				player.transform.position += (crosshair.transform.position - player.transform.position).normalized*distance;
			}else{
				player.transform.position = crosshair.transform.position;
			}

			//make moving forward
			player.rigidbody2D.velocity = player.transform.up*player.rigidbody2D.velocity.magnitude;
		}else{
			player.transform.position += player.transform.up*distance;
		}
		activated = false;
		//GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().boosting(speed, duration);
	}
}
