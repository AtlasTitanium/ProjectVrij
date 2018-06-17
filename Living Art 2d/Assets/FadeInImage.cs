using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour {
	private float transperency = 1.0f;
	void Update(){
		this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transperency);
		transperency -= 0.01f;
		if(transperency <= 0.05){
			transperency = 0.0f;
			this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transperency);
			this.gameObject.SetActive(false);
		}
	}
}
