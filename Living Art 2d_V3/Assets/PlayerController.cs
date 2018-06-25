using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public PlayerMotor Character;
	public GameObject TalkBar;
	public GameObject menySystem;
    private bool jump;
	//[HideInInspector]
	public bool acitvitity = false;
	
	
	void Update(){
		if(Input.GetButtonDown("Escape")){
			Switch();
		}
		//Debug.Log(jump);
		if (!jump){
			jump = Input.GetButtonDown("Jump");
		}
	}


	private void FixedUpdate(){
		if(!acitvitity){
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			Character.Move(x, y, jump);
			jump = false;
		}
	}

	public void Switch(){
		acitvitity = !acitvitity;
		menySystem.SetActive(acitvitity);
		TalkBar.SetActive(!acitvitity);
		Character.enabled = !acitvitity;
	}
}
