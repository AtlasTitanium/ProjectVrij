using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myOwnTextEditor : MonoBehaviour {
	public Image TextObjectImage;
	public Text TextObject;
	public string WhatToSay;
	public float transperency = 0.0f;
	public bool fadein = false;
	public bool fadeout = false;

	void Update(){
		//Debug.Log(transperency);
		if(fadein){
			if(transperency >= 0.5f){
				transperency = 0.5f;
				TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency*2);
				TextObjectImage.color = new Color(TextObjectImage.color.r,TextObjectImage.color.g,TextObjectImage.color.b,transperency);
				fadein = false;
				return;
			}
			TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency*2);
			TextObjectImage.color = new Color(TextObjectImage.color.r,TextObjectImage.color.g,TextObjectImage.color.b,transperency);
			transperency += 0.025f;
		}
		if(fadeout){
			if(transperency <= 0.0f){
				transperency = 0.0f;
				TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency*2);
				TextObjectImage.color = new Color(TextObjectImage.color.r,TextObjectImage.color.g,TextObjectImage.color.b,transperency);
				TextObject.gameObject.SetActive(false);
				TextObjectImage.gameObject.SetActive(false);
				fadeout = false;
				return;
			}
			TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency*2);
			TextObjectImage.color = new Color(TextObjectImage.color.r,TextObjectImage.color.g,TextObjectImage.color.b,transperency);
			transperency -= 0.025f;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			TextObject.gameObject.SetActive(true);
			TextObjectImage.gameObject.SetActive(true);
			TextObject.text = WhatToSay;
			fadein = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			fadeout = true;
		}
	}
}
