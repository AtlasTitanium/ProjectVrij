using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartMenu : MonoBehaviour {

	public Button Begin, Quit;
	public int IntroLevelInteger;
	public Image fadeImage;
	private bool fading = false;
	private float transparency = 0.0f;
	void Start () {
		Button BeginBtn = Begin.GetComponent<Button>();
        BeginBtn.onClick.AddListener(BeginClick);
		Button QuitBtn = Quit.GetComponent<Button>();
        QuitBtn.onClick.AddListener(QuitClick);
	}

	void Update(){
		if(fading){
			fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,transparency);
			transparency += 0.01f;
			if(transparency >= 0.95f){
				transparency = 1.0f;
				fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,transparency);
				StartCoroutine(BeginFade());
				fading = false;
			}
		}
	}
	
	void BeginClick(){
		fadeImage.gameObject.SetActive(true);
		fading = true;
	}
	void QuitClick() {
		Application.Quit();
	}

	IEnumerator BeginFade()
    {
        yield return new WaitForSeconds(1);
		Application.LoadLevel(IntroLevelInteger);
    }
}
