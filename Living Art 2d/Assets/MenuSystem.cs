using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour {
	public Canvas MenuCanvas;
	public Button Return, Help, Quit;
	public int MainMenuInteger;

	void Start () {
		Button ReturnBtn = Return.GetComponent<Button>();
        ReturnBtn.onClick.AddListener(ReturnClick);
		Button HelpBtn = Help.GetComponent<Button>();
        HelpBtn.onClick.AddListener(HelpClick);
		Button QuitBtn = Quit.GetComponent<Button>();
        QuitBtn.onClick.AddListener(QuitClick);
	}
	
	void ReturnClick(){
		GetComponent<PlayerController>().Switch();
	}

	void HelpClick() {
		
	}

	void QuitClick() {
		Application.LoadLevel(MainMenuInteger);
	}
}
