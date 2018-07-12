using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelOnEnter : MonoBehaviour {
	public GameObject objectPlace;
	public int LevelInteger;
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			Application.LoadLevel(LevelInteger);
		}
		if(other.tag == "Pickup"){
			other.gameObject.transform.position = objectPlace.transform.position;
			other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			other.GetComponent<Rigidbody2D>().angularVelocity = 0;
		}
	}
}
