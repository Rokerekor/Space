using UnityEngine;
using System.Collections;

public class EMP : MonoBehaviour {
	float time = 1;
	void Start(){
		LayerMask mask = 1 << 8;
		Collider2D[] stunned = Physics2D.OverlapCircleAll(this.transform.position,3f,mask.value);
		foreach(Collider2D stun in stunned){
			BaseEnemy script = stun.GetComponent<BaseEnemy>();
			if(script != null){
				script.applyStun(3);
			}

		}
	}
	void Update(){
		if(time > 0){
			time-=Time.deltaTime;
		}else{
			Destroy(this.gameObject);
		}
	}

}
