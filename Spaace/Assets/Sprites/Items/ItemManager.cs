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
		itemList.Add(new Item("Shot Count Increase",100));
		itemList.Add(new Item("Damage Increase",40));
		//itemList.Add(new Item("Knockback Increase",20));
		itemList.Add(new Item("Missile Drone",100));
		itemList.Add(new Item("Laser Drone",100));
		itemList.Add(new Item("Bullet Drone",100));
		//itemList.Add(new Item("Cryo Rounds - Slow Enemies",150));
		//itemList.Add(new Item("Ion Rounds - Stun Enemies",250));
	}

	public static Item randomItem(){
		int r = Random.Range(0,itemList.Count);
		return new Item(itemList[r].getName(),itemList[r].getPrice());
	}
	public static void bought(string item,int price){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		PlayerScript script = player.GetComponent<PlayerScript>();
		script.metalTransaction(-price);
		if(item.Equals("Hull Increase")){
			script.increaseHull(10);
		}else if(item.Equals("Speed Increase")){
			script.increaseSpeed(0.5f);
		}else if(item.Equals("Fuel Increase")){
			script.increaseFuel(100);
		}else if(item.Equals("Fire Rate Increase")){
			script.increaseFireRate(1);
		}else if(item.Equals("Shot Count Increase")){
			script.increaseShotCount(1);
		}else if(item.Equals("Damage Increase")){
			script.increaseDamage(2);
		//}else if(item.Equals("Knockback Increase")){
			//script.increaseKnockback(1);
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
			script.repairHull(10);
		}else if(item.Equals("Refueling")){
			script.refuel(100);
		}
	}
}
