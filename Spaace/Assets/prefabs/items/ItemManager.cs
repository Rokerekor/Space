using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	static List<Item> itemList = new List<Item>();
	void Start () {

	}
	// Update is called once per frame
	void Update () {
	
	}
	public static void addItems(){
		itemList.Add(new Item("Hull Increase",30));
		itemList.Add(new Item("Speed Increase",40));
		itemList.Add(new Item("Fuel Increase",20));
		itemList.Add(new Item("Fire Rate Increase",40));
		itemList.Add(new Item("Shot Count Increase",60));
		itemList.Add(new Item("Damage Increase",40));
		itemList.Add(new Item("Missile Drone",70));
		itemList.Add(new Item("Laser Drone",70));
		itemList.Add(new Item("Bullet Drone",70));
	}

	public static Item randomItem(){
		int r = Random.Range(0,itemList.Count);
		return new Item(itemList[r].getName(),itemList[r].getPrice());
	}
	public static bool bought(string item,int price){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		PlayerScript script = player.GetComponent<PlayerScript>();
		if(script.metalTransaction(-price)){
			if(item.Equals("Shot Count Increase")){
				script.increaseShotCount(1);
			}else if(item.Equals("Missile Drone")){
				script.addDrone("missile");
			}else if(item.Equals("Bullet Drone")){
				script.addDrone("bullet");
			}else if(item.Equals("Laser Drone")){
				script.addDrone("laser");
				//}else if(item.Equals("Cryo Rounds - Slow Enemies")){
				//script.upgradeShip("cryo");
				//}else if(item.Equals("Ion Rounds - Stun Enemies")){
				//script.upgradeShip("ion");
			}else if(item.Equals("Hull Repair")){
				if(!script.repairHull(10)){
					script.metalTransaction(price);
					return false;}
			}else if(item.Equals("Refueling")){
				if(!script.refuel(100)){
					script.metalTransaction(price);
					return false;
				}
			}else{return false;}
			return true;
		}else{return false;}
	}
}
