 using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {
	public bool homeBase = false;
	int id = -1;
	Item[] items = new Item[5];
	string[] keys = new string[]{"z","x","c","v","b"};
	int sector = 0;
	// Use this for initialization
	void Start () {
		getItems();
		setText();
	}
	void getItems(){
		if(!homeBase){
			for(int i=0;i<3;i++){
				items[i] = ItemManager.randomItem();
			}
			items[3] = new Item("Hull Repair",5,-1);
			items[4] = new Item("Refueling",5,-1);
		}else{
			items = new Item[2];
			items[0] = new Item("Hull Repair",5,-1);
			items[1] = new Item("Refueling",5,-1);
		}

	}
	void setText(){
		TriggerText textScript = GetComponentInChildren<TriggerText>();
		string itemText = "";
		for(int i=0;i<items.Length;i++){
			if(!items[i].getName().Equals("Sold Out")){
				itemText += (keys[i] + " - $" + items[i].getPrice() + " " + items[i].getName() + "\n\n");
			}else{
				itemText += ("\n\n");
			}
		}
		textScript.setText(itemText);
		//textScript.setText("z - $" + items[0].getPrice() + " " + items[0].getName() + " \n\nx - $" + items[1].getPrice() + " " + items[1].getName() +  "\n\nc - $" + items[2].getPrice() + " " + items[2].getName());
	}
	public void buyItem(int item){
		items[item].bought();
		setText();
	}
	public int getSector(){return sector;}
	public void setSector(int val){sector = val;}
	public int getID(){return id;}
	public void setID(int val){id = val;}
}
