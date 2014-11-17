using UnityEngine;
using System.Collections;

public class FlakRounds : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D col){
		if(col.tag.Equals("Player")){
			//Upgrade Ship Accordingly
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			PlayerScript script = player.GetComponent<PlayerScript>();
			script.increaseBulletWidth(0.6f);
			
			//tell main script to finish up
			this.gameObject.GetComponent<DroppedItem>().pickedUp();
		}
	}
}
