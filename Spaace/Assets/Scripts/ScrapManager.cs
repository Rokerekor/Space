using UnityEngine;
using System.Collections;

public class ScrapManager : MonoBehaviour {

	public GameObject scrap;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void dropScrap(Vector3 position,int worth,Vector3 velocity){
		while(worth > 0){
			GameObject newScrap = (GameObject)Instantiate(scrap,position,new Quaternion(0,0,0,0));
			newScrap.GetComponent<Scrap>().setWorth(1);
			worth--;
			newScrap.rigidbody2D.velocity = velocity;
		}
	}
}
