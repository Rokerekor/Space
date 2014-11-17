using UnityEngine;
using System.Collections;

public class ShieldItem : MonoBehaviour {
	public GameObject shield;

	public void OnTriggerEnter2D(Collider2D col){
		if(col.tag.Equals("Player")){
			//Upgrade Ship Accordingly
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			GameObject existingShield = GameObject.FindGameObjectWithTag("Shield");
			if(existingShield == null){
				GameObject newShield = (GameObject)Instantiate(shield,player.transform.position,new Quaternion(0,0,0,0));
			}else{
				Debug.Log(existingShield);
				existingShield.GetComponent<Shield>().addRing();
			}
			//tell main script to finish up
			this.gameObject.GetComponent<DroppedItem>().pickedUp();
		}
	}
}
