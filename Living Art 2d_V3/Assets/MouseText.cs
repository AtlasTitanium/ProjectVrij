using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseText : MonoBehaviour {
	public GameObject TextObject;
	private Vector2 MousePlace;
	private Vector3 MouseRotation = new Vector3(0,0,0);
	public GameObject TextPlaceCanvas;

	void Update () {
		MousePlace = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		//Debug.Log(MousePlace);
	}

	public void DisplayText(string Text){
		TextObject.transform.position = MousePlace;
		TextObject.transform.position = new Vector2(TextObject.transform.position.x,TextObject.transform.position.y + 0.5f);
		TextObject.GetComponent<Text>().text = Text;
	}
}
