using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickToGum : MonoBehaviour {

	private Color OriginalColor;
	public GameObject Gum;
	public PickupObject PickObj;
	private bool onlyOnce = true;
	private GameObject PickUpPlace;
	public gravityMonster MonsterScript;
	void Update(){
		if(Gum != null){
			if(Input.GetButtonDown("Fire3")){
				Gum.tag = "UsedGum";
				Gum.layer = 0;
				//Debug.Log("sticktoGum");
				this.transform.parent = Gum.transform;
				//MonsterScript.blocked = false;

				this.transform.rotation = Gum.transform.rotation;
				if(PickObj != null){
					PickObj.Object.GetComponent<Collider2D>().isTrigger = true;
					PickObj.Object = null;
					PickObj.picked = false;	
					//this.enabled = false;
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(other.transform.tag == "Gum"){
			Gum = other.gameObject;
			if(onlyOnce){
				OriginalColor = Gum.transform.parent.GetComponent<SpriteRenderer>().color;
				onlyOnce = false;
			}
			Gum.transform.parent.GetComponent<SpriteRenderer>().color = Color.blue;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(Gum != null){
			Gum.transform.parent.GetComponent<SpriteRenderer>().color = OriginalColor;
			onlyOnce = true;
			Gum = null;
		}
	}
}
