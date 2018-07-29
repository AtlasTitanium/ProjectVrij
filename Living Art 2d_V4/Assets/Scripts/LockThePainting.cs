using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockThePainting : MonoBehaviour {
	public bool locked = false;
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Pushable"){
			other.gameObject.SetActive(false);
			locked = true;
			StartCoroutine(WaitForLock());
		}
	}

	IEnumerator WaitForLock(){
		yield return new WaitForSeconds(5);
		locked = false;
	}
}
