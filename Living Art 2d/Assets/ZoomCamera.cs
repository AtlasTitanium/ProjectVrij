using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZoomCamera : MonoBehaviour {
	private float fadeTransperency = 1.0f;
	private Image fadeImage;
	void Awake () {
		fadeImage = this.GetComponent<Image>();
	}
	void Update () {
		fadeImage.color = new Color(0,0,0,fadeTransperency);
		fadeTransperency -= 0.1f * Time.deltaTime;
		if(fadeTransperency <= 0.05f){
			fadeTransperency = 0;
			this.transform.parent.gameObject.SetActive(false);
			this.enabled = false;
		}
	}
	
}
