using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	string 	name = 	"";
	int 	price = 0;
	int 	quantity = 1;

	public Item(string name,int price){
		this.name = name;
		this.price = price;
	}
	public Item(string name,int price,int quantity){
		this.name = name;
		this.price = price;
		this.quantity = quantity;
	}
	public void bought(){
		ItemManager.bought(name,price);
		if(quantity == 1){
			name = "Sold Out";
			price = 0;
		}else if(quantity > 1){
			quantity--;
		}
	}
	public int getPrice(){return price;}
	public string getName(){return name;}	
}