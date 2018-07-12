using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour {
	public Canvas MenuCanvas;
	public Button Return, Reset, Help, Quit;
	public int MainMenuInteger;
	public GameObject HelpImage;
	private bool helpCanvas = false;
	public Image ShadowText;
	public Image txt;

	void Start () {
		Button ReturnBtn = Return.GetComponent<Button>();
        ReturnBtn.onClick.AddListener(ReturnClick);
		Button HelpBtn = Help.GetComponent<Button>();
        HelpBtn.onClick.AddListener(HelpClick);
		Button QuitBtn = Quit.GetComponent<Button>();
        QuitBtn.onClick.AddListener(QuitClick);
		Button ResetBtn = Reset.GetComponent<Button>();
        ResetBtn.onClick.AddListener(ResetClick);
	}

	void Update(){
		if(helpCanvas){
			if(Input.GetButtonDown("Escape")){
				Return.gameObject.SetActive(true);
				Help.gameObject.SetActive(true);
				Quit.gameObject.SetActive(true);
				ShadowText.gameObject.SetActive(true);
				txt.gameObject.SetActive(true);
				HelpImage.SetActive(false);
				helpCanvas = false;
				GetComponent<PlayerController>().Switch();
			}
		}
	}
	
	void ReturnClick(){
		GetComponent<PlayerController>().Switch();
	}

	void ResetClick(){
		 Application.LoadLevel(Application.loadedLevel);
	}

	void HelpClick() {
		Return.gameObject.SetActive(false);
		Help.gameObject.SetActive(false);
		Quit.gameObject.SetActive(false);
		ShadowText.gameObject.SetActive(false);
		txt.gameObject.SetActive(false);
		HelpImage.SetActive(true);
		helpCanvas = true;
	}

	void QuitClick() {
		Application.LoadLevel(MainMenuInteger);
	}
}
