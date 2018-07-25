using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinePlace : MonoBehaviour {
	public GameObject Trampoline;
	public GameObject Gum;
	public bool hasGum;
	private SpriteRenderer SprRnd;
	private float Transparency = 0.0f;
	private bool TransparencyUp = false;
	public int WaitTime = 30;
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
	public void CREATE(){
		Destroy(Gum);
		Gum = null;
		Trampoline.SetActive(true);
		Destroy(this.gameObject);
	}

	IEnumerator WaitForSecond(){
		yield return new WaitForSeconds(WaitTime);
		TransparencyUp = true;
	}
}
