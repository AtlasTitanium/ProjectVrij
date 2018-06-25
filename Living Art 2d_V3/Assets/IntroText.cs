using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroText : MonoBehaviour {
	public string[] Texts;
	public float TimeForText;
	public float TimeInBetween;
	public float transparencySpeed = 0.1f;
	private Text TextObject;
	private float transparency = 0f;
	private bool WaitForText = false;
	private bool FadeIn = true;
	private bool FadeOut = false;
	private int i = 0;
	private float fadeTransperency = 0.0f;
	public Image fadeImage;
	// Use this for initialization
	void Start () {
		TextObject = this.GetComponentInChildren<Text>();
		this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transparency);
	}
	
	// Update is called once per frame
	void Update () {
		if(!WaitForText){
			if(i < Texts.Length){
				if(Texts[i] != null){
					TextObject.text = Texts[i];	
					StartCoroutine(ForText());
					Debug.Log("changeText");
				}
			}
		}
		if(i < Texts.Length){
			if(FadeIn){
				Debug.Log("FadeIn");
				transparency += transparencySpeed;
				TextObject.GetComponent<Text>().color = new Color(TextObject.GetComponent<Text>().color.r,TextObject.GetComponent<Text>().color.g,TextObject.GetComponent<Text>().color.b,transparency*2);
				this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transparency);
				if(transparency >= 0.5f){
					StartCoroutine(FadIn());
					FadeIn = false;
				}
			} 
			if(FadeOut){
				Debug.Log("FadeOut");
				transparency -= transparencySpeed;
				TextObject.GetComponent<Text>().color = new Color(TextObject.GetComponent<Text>().color.r,TextObject.GetComponent<Text>().color.g,TextObject.GetComponent<Text>().color.b,transparency*2);
				this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,transparency);
				if(transparency <= 0.0f){
					StartCoroutine(FadOut());
					FadeOut = false;
				}
			}
		} else {
			fadeImage.GetComponent<FadeInImage>().FadeOut();
			if(fadeImage.GetComponent<FadeInImage>().transperency >= 0.99){
				Application.LoadLevel(2);
			}
		}
	}

	IEnumerator ForText(){
		WaitForText = true;
        yield return new WaitForSeconds(TimeForText);
		i += 1;
		WaitForText = false;
    }
	IEnumerator FadIn(){
        yield return new WaitForSeconds(TimeInBetween);
		Debug.Log("ChangeFade");
		FadeOut = true;
    }

	IEnumerator FadOut(){
        yield return new WaitForSeconds(TimeInBetween);
		Debug.Log("ChangeFade");
		FadeIn = true;
    }
}
