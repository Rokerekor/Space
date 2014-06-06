using UnityEngine;
using System.Collections;

public class StarManager : MonoBehaviour {
	public GameObject Star;
	
	public int starCount = 500;
	public float minStarSize = 0.001f;
	public float maxStarSize = 0.005f;
	public int xGridLocation = 0;
	public int yGridLocation = 0;
	float xVal = 0;
	float yVal = 0;
	float height = 0;
	float width = 0;
	public float paralax = 0.01f;
	bool pauseFix = false;
	GameObject camera;

	Vector3 lastCamPos;
	
	void Start () {
		camera = Camera.main.gameObject;
		height = Camera.main.orthographicSize * 2f;
		width = height * Screen.width / Screen.height;
		for(int i=0;i<starCount;i++){
			spawnStar();
		}
		lastCamPos = camera.transform.position;
	}
	
	void Update () {
		if(Time.timeScale == 1){
			move(false);
			refreshStars();
			pauseFix = false;
		}else if(!pauseFix){
			move(true);
			pauseFix = true;
		}
	}
	void move(bool reversed){
		Vector3 camPos = camera.transform.position;
		
		int xFix = 0;
		int yFix = 0;
		xVal -= (camPos.x - lastCamPos.x)*Time.deltaTime*paralax;
		yVal -= (camPos.y - lastCamPos.y)*Time.deltaTime*paralax;
		this.transform.position = new Vector3(camPos.x + xGridLocation*width + xVal,camPos.y + yGridLocation*height + yVal,0);
		if(this.transform.position.x - camPos.x > width*1.5){
			xFix -= 3;
		}else if(this.transform.position.x - camPos.x < -width*1.5){
			xFix += 3;
		}else if(this.transform.position.y - camPos.y > height*1.5){
			yFix -= 3;
		}else if(this.transform.position.y - camPos.y < -height*1.5){
			yFix += 3;
		}
		if(reversed){
			xFix=-xFix;
			yFix=-yFix;
		}
		xGridLocation+=xFix;
		yGridLocation+=yFix;

		lastCamPos = camera.transform.position;
	}
	void refreshStars(){	
		Vector3 camPos = camera.transform.position;
		if(this.transform.position.x - camPos.x > width || this.transform.position.x - camPos.x < -width || this.transform.position.y - camPos.y > height || this.transform.position.y - camPos.y < -height*1.5){
			int count = this.transform.childCount;
			int random = Random.Range(0,count);
			Destroy(this.transform.GetChild(random).gameObject);
			spawnStar();
		}
	}
	void spawnStar(){
		float x = Random.Range(-width/2f,width/2f);
		float y = Random.Range(-height/2f,height/2f);
		//Debug.Log(x + y);
		float starSize = Random.Range(minStarSize*paralax/10,maxStarSize*paralax/10);
		GameObject newStar = (GameObject)Instantiate(Star,new Vector3(this.transform.position.x + x,this.transform.position.y + y,0),new Quaternion(0,0,0,0));
		newStar.transform.localScale = new Vector3(starSize, starSize, newStar.transform.localScale.y);
		newStar.transform.parent = this.gameObject.transform;
	}
}
