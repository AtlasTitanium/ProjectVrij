using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZoomCamera : MonoBehaviour {
	private float fadeTransperency = 1.0f;
	private Image fadeImage;
	public PlayerController playerController;
	public PlayerMotor playerMotor;
	void Awake () {
		fadeImage = this.GetComponent<Image>();
	}
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			fadeTransperency = 0;
			playerController.enabled = true;
			playerMotor.enabled = true;
			this.transform.parent.gameObject.SetActive(false);
			this.enabled = false;
		}
		fadeImage.color = new Color(0,0,0,fadeTransperency);
		fadeTransperency -= 0.05f * Time.deltaTime;
		if(fadeTransperency <= 0.05f){
			fadeTransperency = 0;
			playerController.enabled = true;
			playerMotor.enabled = true;
			this.transform.parent.gameObject.SetActive(false);
			this.enabled = false;
		}
	}
	
}
