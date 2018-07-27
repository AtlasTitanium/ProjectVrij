using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {
	public GameObject Player;
	public MouseText Mouse;

	void Update(){
		if(Player != null){
			if(Mouse != null){
				Mouse.DisplayText("Hold Space to go up");
			}
			if(Input.GetButton("Jump")){
				Debug.Log("goUp");
				Player.GetComponent<PlayerController>().enabled = false;
				Player.GetComponent<PlayerMotor>().enabled = false;
				Player.GetComponent<Rigidbody2D>().AddForce(Player.transform.up * 2f);
				//Player.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 0.000000000001f);				
			}
			Player.GetComponent<PlayerController>().enabled = true;
			Player.GetComponent<PlayerMotor>().enabled = true;
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			Player = other.gameObject;
			Player.GetComponent<Rigidbody2D>().gravityScale = 0;
			Player.GetComponent<Rigidbody2D>().drag = 1f;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			Player.GetComponent<Rigidbody2D>().drag = 0f;
			Player.GetComponent<Rigidbody2D>().gravityScale = 1;
			Player = null;
			if(Mouse != null){
				Mouse.DisplayText("");
			}
		}
	}
}
