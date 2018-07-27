using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGuMPLace : MonoBehaviour {

	public bool hasGum;
	private SpriteRenderer SprRnd;
	private float Transparency = 0.0f;
	private bool TransparencyUp = false;
	public int WaitTime = 30;
	public bool ShowText = false;
	void Start(){
		SprRnd = GetComponent<SpriteRenderer>();
		StartCoroutine(WaitForSecond());
	}
	void Update(){
		SprRnd.color = new Color(SprRnd.color.r,SprRnd.color.g,SprRnd.color.b,Transparency);
		if(TransparencyUp){
			if(Transparency <= 0.28){
				Transparency += 0.001f;
			}
		}
	}

	IEnumerator WaitForSecond(){
		yield return new WaitForSeconds(WaitTime);
		TransparencyUp = true;
		ShowText = true;
	}
}
