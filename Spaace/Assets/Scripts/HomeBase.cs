using UnityEngine;
using System.Collections;

public class HomeBase : MonoBehaviour {
	Vector3 forcedPosition;//= new Vector3(2.553201f,1.997588f,0f);
	Quaternion forcedRotation = new Quaternion(0,0,0.7f,0.7f);
	GameObject clonedStation;
	bool forcing = false;
	GameObject dock;
	GameObject gui;
	GameObject player;
	void Start () {
		forcedPosition = transform.position;
		Debug.Log(forcedPosition);
		forcedPosition.x += GetComponent<BoxCollider2D>().center.x;
		forcedPosition.x += GetComponent<BoxCollider2D>().size.x/2;
		Debug.Log(forcedPosition);
		forcedPosition.y += GetComponent<BoxCollider2D>().center.y;
		//forcedPosition.y -= GetComponent<BoxCollider2D>().size.y/2;
		Debug.Log(forcedPosition);
		dock = GameObject.FindGameObjectWithTag("Dock");
		dock.SetActive(false);
	}
	void Update(){

		Debug.Log(GameObject.FindGameObjectWithTag("Player").rigidbody2D.velocity);
		if(forcing){
			//player.transform.position = forcedLocation;
			player.transform.position = Vector3.Lerp(player.transform.position,forcedPosition,0.05f);
			player.transform.rotation = Quaternion.Lerp(player.transform.rotation,forcedRotation,0.05f);
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.transform.position,new Vector3(forcedPosition.x,forcedPosition.y,Camera.main.transform.position.z),0.05f);
		}
	}
	void OnGUI() {
		Event e = Event.current;
		if (e.isKey){
			if(e.type == EventType.KeyDown){
				if(e.keyCode == KeyCode.Z){
					if(forcing){
						closeBase();
					}
					else if(this.GetComponent<TriggerText>().isTriggered()){
						openBase();
					}
				}
				if(e.keyCode == KeyCode.Escape){
					if(forcing){
						closeBase();
					}
				}
			}
		}
	}
	void openBase(){
		player = GameObject.FindGameObjectWithTag("Player");
		player.rigidbody2D.velocity = Vector2.zero;
		player.rigidbody2D.angularVelocity = 0;
		dock.SetActive(true);
		//dock.transform.position = transform.position;
		forcing = true;
		Time.timeScale = 0;
		GameObject clonedStation = (GameObject)Instantiate(this.gameObject,this.transform.position,this.transform.rotation);
		SpriteRenderer[] sR = clonedStation.GetComponentsInChildren<SpriteRenderer>();
		Destroy(clonedStation.GetComponent<HomeBase>());
		foreach(SpriteRenderer s in sR){
			s.sortingOrder+=10;
		}
		gui = GameObject.FindGameObjectWithTag("GUI");
		gui.SetActive(false);
		GameObject.FindGameObjectWithTag("fader").GetComponent<cameraFunctions>().fadeToBlack(0.04f);
		Screen.showCursor = true;
	}
	void closeBase(){
		dock.SetActive(false);
		forcing = false;
		Time.timeScale = 1;
		Destroy(clonedStation);
		gui.SetActive(true);
		GameObject.FindGameObjectWithTag("fader").GetComponent<cameraFunctions>().fadeToClear(0.04f);
		Screen.showCursor = false;
	}

}
