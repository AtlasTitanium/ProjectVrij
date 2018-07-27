using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SticktoPainting : MonoBehaviour {

	private Color OriginalColor;
	public GameObject Hole;
	public PickupObject PickObj;
	private bool onlyOnce = true;
	private GameObject PickUpPlace;
	public gravityMonster MonsterScript;
	public MouseText mouse;
	void Update(){
		if(PickObj.Object != null){
			if(Hole != null){
				mouse.DisplayText("Press Left Mouse to place object");
				//Debug.Log("there's gum");
				if(Input.GetButtonDown("Fire3")){
					mouse.DisplayText("");
					Hole.GetComponent<PaintingHole>().FillHole();
					Destroy(this.gameObject);
					PickObj.picked = false;
				}
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(other.transform.tag == "PaintingHole"){
			if(other.GetComponent<PaintingHole>().hasGum){
				Hole = other.gameObject;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(Hole != null){
			mouse.DisplayText("");
			Hole = null;
		}
	}
}
