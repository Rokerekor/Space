using UnityEngine;
using System.Collections;

public class SectorsManager: MonoBehaviour {

	public Sector[] sectors;
	int currentSector = 0;
	int exploredSector = 1;
	float counter;
	GameObject gui;
	void Start () {
		gui = GameObject.FindGameObjectWithTag("GUI");
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if(counter >= 1){
			checkSector();
		}
	}
	void checkSector(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 pos = player.transform.position;
		float distance = new Vector2(pos.x,pos.y).magnitude;
		//Debug.Log(distance);
		for(int i=0;i<sectors.Length;i++){
			if(distance >= sectors[i].innerLimit && distance < sectors[i].outerLimit){
				if(currentSector != i){
					currentSector = i;
					gui.transform.FindChild("centerText").GetComponent<centerText>().showText(sectors[i].sectorTitle,1);
					return;
				}
				if(exploredSector < i){
					exploredSector = i;
					return;
				}
			}
		}
	}
	public void stationClear(int sector){

		if(sectors[sector].GetComponent<Sector>().stationClear() >= 0.5){

		}

	}
	public int getSector(){return currentSector;}
	public int getExploredSector(){return exploredSector;}
}
