using UnityEngine;
using System.Collections;

public class Sector : MonoBehaviour {

	public int innerLimit;
	public int outerLimit;
	public GameObject[] stations;
	public GameObject[] stores;
	int stationClearCount = 0;
	public int stationCount;
	public int storeCount;
	public string sectorTitle;
	public float stationClear(){
		stationClearCount++;
		Debug.Log((float)stationClearCount/stationCount);
		return stationClearCount/stationCount;
	}

}
