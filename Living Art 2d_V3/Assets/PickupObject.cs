using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour {
	public bool picked = false;
	public GameObject Object;
	private Color OriginalColor;
	void Update(){
		if(Object != null){
			if(!picked){
				if(Input.GetButtonDown("Fire1")){
					if(Object.transform.parent != null){
						if(Object.transform.parent.tag == "UsedGum"){
							Object.transform.parent.tag = "Gum";
							Object.transform.parent.gameObject.layer = 11;
						}
					}
					//Debug.Log("pickup");
					Object.GetComponent<stickToGum>().enabled = true;
					Object.transform.parent = this.transform;
					Object.GetComponent<Rigidbody2D>().gravityScale = 0;
					Object.GetComponent<Collider2D>().isTrigger = true;
					Object.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
					Object.transform.localPosition = new Vector3(0,0,0);
					picked = true;
				}
			} else {
				Object.transform.position = transform.position;
				if(Input.GetButtonDown("Fire1")){
					//Debug.Log("drop");
					Object.transform.parent = null;
					Object.GetComponent<Rigidbody2D>().gravityScale = 3;
					Object.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
					Object.GetComponent<Collider2D>().isTrigger = false;
					Object = null;
					picked = false;
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(other.transform.tag == "Pickup"){
			Object = other.gameObject;
		}
	}

	void OnTriggerExit2D(){
		if(Object != null){
			Object = null;
		}
	}
}
