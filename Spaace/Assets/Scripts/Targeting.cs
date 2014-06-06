using UnityEngine;
using System.Collections;

public class Targeting : MonoBehaviour {

	public GameObject crosshair;
	public GameObject target;
	GameObject cross;
	GameObject tempTarget;
	bool onTarget = false;
	bool locked = false;
	int chargedVal = 1;
	void Start () {
	
	}
	
	void Update () {
		if(Input.GetMouseButton(1)){
			if(onTarget){
				if(tempTarget != null){
					if(target != null && tempTarget != target){
						target = tempTarget;
						createCrosshair();
						locked = true;
					}else if(target == null){
						target = tempTarget;
						createCrosshair();
						locked = true;
					}
				}
			}
		}
		if(locked){
			if(target == null){
				locked = false;
				Destroy(cross);
			}else{
				cross.transform.position = target.transform.position;
				cross.transform.Rotate(Vector3.forward*chargedVal);
			}
		}

	}
	public void charged(bool val){
		if(val){
			chargedVal = 5;
		}else{
			chargedVal = 1;
		}
	}
	void OnTriggerStay2D(Collider2D collide){
		if(collide.tag.Equals("Enemy") || collide.tag.Equals("Asteroid")){
			onTarget = true;
			tempTarget = collide.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D collide){
		if(collide.tag.Equals("Enemy") || collide.tag.Equals("Asteroid")){
			onTarget = false;
		}
	}
	void createCrosshair(){
		if(cross != null){
			Destroy(cross);
		}	
		GameObject newCross = (GameObject)Instantiate(crosshair,transform.position,transform.rotation);
		cross = newCross;
		cross.transform.localScale = new Vector3((cross.transform.localScale.x + target.renderer.bounds.size.x)*3/4,
		                                         (cross.transform.localScale.y + target.renderer.bounds.size.x)*3/4,
		                                         (cross.transform.localScale.x + target.renderer.bounds.size.x)*3/4);
	}
	public Vector3 targetPosition(){return cross.transform.position;}
	public bool isLocked(){return locked;}
	public void setLocked(bool val){
		locked = val;
		if(val == false){
			locked = false;
			Destroy(cross);
			target = null;
		}
	}
}