using UnityEngine;
using System.Collections;

public class StunBoltItem : MonoBehaviour {

	public GameObject electricField;
	
	public void OnTriggerEnter2D(Collider2D col){
		if(col.tag.Equals("Player")){
			//Upgrade Ship Accordingly
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			GameObject existingField = GameObject.FindGameObjectWithTag("ElectricField");
			if(existingField == null){
				GameObject newField = (GameObject)Instantiate(electricField,player.transform.position,new Quaternion(0,0,0,0));
			}else{
				existingField.GetComponent<StunBolt>().upgrade();
			}
			//tell main script to finish up
			this.gameObject.GetComponent<DroppedItem>().pickedUp();
		}
	}
}
