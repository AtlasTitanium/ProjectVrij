using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myOwnTextEditor : MonoBehaviour {
	public Image TextObjectImage;
	public Text TextObject;
	public gravityMonster Monster;
	public bool forMonster = false;
	public bool forBlocked = false;
	public bool Always = false;
	private float transperency = 0.0f;
	private bool fadein = false;
	private bool fadeout = false;
	private bool timesUp = false;

	void Update(){
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
			if(forMonster){
				if(forBlocked){
					if(Monster.GetComponent<gravityMonster>().blocked == true){
						fadeout = false;
						TextObject.gameObject.SetActive(true);
						TextObjectImage.gameObject.SetActive(true);
						fadein = true;
					}
				} else {
					if(Monster.GetComponent<gravityMonster>().blocked == false){
						fadeout = false;
						TextObject.gameObject.SetActive(true);
						TextObjectImage.gameObject.SetActive(true);
						fadein = true;
					}
				}
			} else {
				if(Always){
					fadeout = false;
					TextObject.gameObject.SetActive(true);
					TextObjectImage.gameObject.SetActive(true);
					fadein = true;
				} else {
					if(!timesUp){
						fadeout = false;
						TextObject.gameObject.SetActive(true);
						TextObjectImage.gameObject.SetActive(true);
						fadein = true;
					} else {
						fadein = false;
						fadeout = true;
					}
				}
			}
			
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			if(!Always){
				StartCoroutine(Time());
			} else {
				fadein = false;
				fadeout = true;
			}
		}
	}

	IEnumerator Time(){
		yield return new WaitForSeconds(2f);
		timesUp = true;
	}
}
