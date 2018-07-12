using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSpriteOrder : MonoBehaviour {
	public Image fadeImage;
	public Text TextObject;
	public string[] Texts;
	public GameObject credits;
	private float creditsheight;
	
	public Image backgroundImage;
	public Sprite[] OrderSprites;
	public float speedSwtich;
	private bool fadeoldImage = true;
	private bool fadeNewImage = false;
	private int currentsprite = 0;
	private float transperency = 0.0f;
	public float fadespeed = 0.01f;
	private int i = 0;

	void Start(){
		creditsheight = credits.transform.position.y;
		backgroundImage.color = new Color(255,255,255,transperency);
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel(0);
		}
		if(i < Texts.Length){
			if(Texts[i] != null){
				backgroundImage.sprite = OrderSprites[i];
				TextObject.text = Texts[i];
				Debug.Log("changeText");
			}
			if(fadeoldImage){
				if(transperency >= 1.05f){
					transperency = 1.0f;
					backgroundImage.color = new Color(255,255,255,transperency);
					TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency);
					fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,transperency/2);
					StartCoroutine(FadOut());
					fadeoldImage = false;
					return;
				}
				backgroundImage.color = new Color(255,255,255,transperency);
				TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency);
				fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,transperency/2);
				transperency += fadespeed;
			}
			if(fadeNewImage){
				if(transperency <= -0.01f){
					transperency = 0.0f;
					backgroundImage.color = new Color(255,255,255,transperency);
					TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency);
					fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,transperency/2);
					StartCoroutine(FadIn());
					fadeNewImage = false;
					return;
				} 
				backgroundImage.color = new Color(255,255,255,transperency);
				TextObject.color = new Color(TextObject.color.r,TextObject.color.g,TextObject.color.b,transperency);
				fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,transperency/2);
				transperency -= fadespeed;
			}
		} else{
			credits.transform.position = new Vector2(credits.transform.position.x, creditsheight);
			creditsheight += 1f;
			if(creditsheight >= 1200){
				Application.LoadLevel(0);
			}
		}
		
	}

	IEnumerator FadIn(){
		 yield return new WaitForSeconds(0);
		Debug.Log("ChangeFade");
		i += 1;
		fadeoldImage = true;
		fadeNewImage = false;
	}
	IEnumerator FadOut(){
        yield return new WaitForSeconds(speedSwtich);
		Debug.Log("ChangeFade");
		fadeoldImage = false;
		fadeNewImage = true;
    }
}
