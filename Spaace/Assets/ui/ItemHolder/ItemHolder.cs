using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemHolder : MonoBehaviour {
	public GameObject iHI;

	const float highY = -4.5f;
	const float lowY = -5f;
	const float xInc = 0.5f;

	const int textSpaceX = 30;
	const int textSpaceY = 10;

	List<GameObject> items = new List<GameObject>();
	List<Sprite> sprites = new List<Sprite>();
	List<int> quantity = new List<int>();
	int val = 0;

	float x = 0;
	float y = -4.5f;


	public void addItem(Sprite sprite){
		if(sprites.Contains(sprite)){
			int val = sprites.IndexOf(sprite);
			quantity[val]++;
			setText(val);
		}else{
			GameObject newItem = (GameObject)Instantiate(iHI, Vector3.zero,new Quaternion(0,0,0,0));
			newItem.GetComponent<SpriteRenderer>().sprite = sprite;
			newItem.transform.localScale = new Vector2(0.3f,0.3f);
			newItem.transform.parent = Camera.main.transform;
			newItem.transform.localPosition = new Vector3(x,y,1);
			if(y == lowY){
				y = highY;
				x+=xInc;
			}else{
				y = lowY;
			}
			sprites.Add(sprite);
			items.Add(newItem);
			quantity.Add(1);
			//newItem.transform.parent = this.gameObject;
		}
	}
	void setText(int val){
		items[val].transform.FindChild("text").GetComponent<TextMesh>().text = "x" + quantity[val];
	}
}
