using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour {
	public float transperency = 1.0f;
	private bool fade = true;
	void Update(){
		if(fade){
			this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transperency);
			transperency -= 0.01f;
			if(transperency <= 0.05){
				transperency = 0.0f;
				this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transperency);
				this.gameObject.SetActive(false);
			}
		} else {
			this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transperency);
			transperency += 0.01f;
			if(transperency >= 0.99f){
				transperency = 1.0f;
				this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transperency);
				//this.gameObject.SetActive(false);
			}
		}
	}

	public void FadeIn(){
		fade = true;
	}
	public void FadeOut(){
		this.gameObject.SetActive(true);
		fade = false;
	}
}
