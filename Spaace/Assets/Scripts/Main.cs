using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Main : MonoBehaviour {
	GameObject player;
	GameObject crosshair;
	bool tutorialMode = true;
	int tutorialStep = 1;
	Vector3 fixedPos;
	Vector3 velocity = Vector3.zero;
	Vector3 shakeTarget;
	Vector3 shakePos;
	float glowTime = 0;
	float shakeIntensity = 0;
	float shakeTime = 0;
	List<GameObject> colList = new List<GameObject>();

	void Start () {
		Screen.showCursor = false;
		player = GameObject.FindGameObjectWithTag("Player");
		crosshair = GameObject.FindGameObjectWithTag("Crosshair");
		fixedPos = new Vector3(this.transform.position.x,this.transform.position.y,player.transform.position.z);
		shakeTarget = new Vector3(this.transform.position.x,this.transform.position.y,player.transform.position.z);
		shakePos = new Vector3(this.transform.position.x,this.transform.position.y,player.transform.position.z);
	}
	
	void FixedUpdate () {
		if(Time.timeScale > 0){
			cameraShakeEffect();
			cameraGlowEffect();
			updatePositions();
			if(tutorialMode){
				tutorial();
			}
		}
	}
	void OnTriggerStay2D(Collider2D col){
		if(col.tag.Equals("Enemy")){
			colList.Add(col.gameObject);
		}
	}
	void updatePositions(){
		Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		crosshair.transform.position = new Vector3(camPos.x,camPos.y,0);

//		if(crosshair.GetComponent<Targeting>().isLocked()){
//			Vector3 difference = player.transform.position - crosshair.GetComponent<Targeting>().targetPosition();
//			if(Mathf.Abs(difference.x) > 17 || Mathf.Abs(difference.y) > 10){
//				crosshair.GetComponent<Targeting>().setLocked(false);
//			}
//			fixedPos = Vector3.SmoothDamp(fixedPos, player.transform.position - difference/2f, ref velocity, 0.3f);
//		}else{
			Vector3 targetPos = player.transform.position;
			for(int i=0;i<colList.Count;i++){
				if(colList[i] != null){
					targetPos += colList[i].transform.position;
					targetPos += player.transform.position*2;
				}
			}
			targetPos /= 1+colList.Count*3; 
			fixedPos = Vector3.SmoothDamp(fixedPos, targetPos, ref velocity, 0.3f,10);
			colList.Clear();
//			Vector3 difference = player.transform.position - camPos;
//			if(difference.x > 17){
//				difference.x = 17;
//			}else if(difference.x < -17){
//				difference.x = -17;
//			}else if(difference.y > 17){
//				difference.y = 10;
//			}else if(difference.y < -10){
//				difference.y = -10;
//			}
//			fixedPos = Vector3.SmoothDamp(fixedPos, player.transform.position - difference/4f, ref velocity, 0.3f);
//		}
		this.transform.position = new Vector3(fixedPos.x,fixedPos.y,-10);
	}
	void tutorial(){
		if(tutorialStep == 1){
//			if(instruction1.transform.position.x > 15 || instruction1.transform.position.x < -15 || instruction1.transform.position.y < -10 || instruction1.transform.position .y > 10){
//				//Destroy(instruction1);
//			}
		}
	}
	void cameraShakeEffect(){
		if(shakeTime > 0){
			fixedPos += new Vector3(Random.Range(-shakeIntensity,shakeIntensity),Random.Range(-shakeIntensity,shakeIntensity),player.transform.position.z);
			shakeTime-=Time.deltaTime;
			shakeIntensity=shakeIntensity*0.75f;
		}else{
			shakeIntensity=0;
		}
	}

	void cameraGlowEffect(){
		if (glowTime > 0) {
			Camera.main.GetComponent<GlowEffect> ().enabled = true;
			glowTime -= Time.deltaTime;
			Camera.main.GetComponent<GlowEffect>().setTint(glowTime);
			//Camera.main.GetComponent<Vignetting>().setChroma(glowTime);
		} else {
			Camera.main.GetComponent<GlowEffect> ().enabled = false;
			}
		}

	public void Glow(float time){
		glowTime += time;
	}


	public void cameraKick(Vector3 direction){
		this.transform.position += direction/50;
		fixedPos += direction/30;
	}
	public void cameraShake(float intensity,float time){
		shakeIntensity += intensity;
		shakeTime += time;
	}
}
