using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class stickToGum : MonoBehaviour {

	private Color OriginalColor;
	public GameObject Gum;
	public PickupObject PickObj;
	private bool onlyOnce = true;
	private GameObject PickUpPlace;
	public gravityMonster MonsterScript;
	public MouseText mouse;
	void Update(){
		if(PickObj.Object != null){
			if(Gum != null){
				mouse.DisplayText("Press Right Mouse to place object");
				//Debug.Log("there's gum");
				if(Input.GetButtonDown("Fire3")){
					PickObj.Object.transform.parent = null;
					PickObj.Object.transform.position = new Vector3(PickObj.Object.transform.position.x,PickObj.Object.transform.position.y,PickObj.Object.transform.position.z -1);
					PickObj.Object.transform.localScale = PickObj.size;
					Gum.tag = "UsedGum";
					Gum.layer = 0;
					//Debug.Log("sticktoGum");
					this.transform.parent = Gum.transform;
					//MonsterScript.blocked = false;

					this.transform.rotation = Gum.transform.rotation;
					if(PickObj != null){
						mouse.DisplayText("");
						PickObj.Object.GetComponent<Collider2D>().isTrigger = true;
						PickObj.Object = null;
						PickObj.picked = false;	
						//this.enabled = false;
					}
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(other.transform.tag == "Gum"){
			Gum = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(Gum != null){
			mouse.DisplayText("");
			Gum = null;
		}
	}
}
