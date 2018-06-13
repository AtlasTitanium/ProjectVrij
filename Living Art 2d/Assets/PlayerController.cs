using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public PlayerMotor Character;
    private bool jump;
	
	
	private void Update(){
		Debug.Log(jump);
		if (!jump){
			jump = Input.GetButtonDown("Jump");
		}
	}


	private void FixedUpdate(){
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		float h = Input.GetAxis("Horizontal");
		Character.Move(h, crouch, jump);
		jump = false;
	}
}
