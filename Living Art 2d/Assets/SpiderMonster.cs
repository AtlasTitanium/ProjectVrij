using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpiderMonster : MonoBehaviour {
	public GameObject GumObject;
	public GameObject monster;
	public float speed;
	private float transparesy = 0.1f;
	private bool howfast = false;
	private bool waitForNextFrame = false;

	void Update () {
		if(GumObject == null){
			if(monster != null){
				float step = speed * Time.deltaTime;
				this.transform.position = Vector2.MoveTowards(this.transform.position, monster.transform.position, step);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
				Debug.Log("following monster");
			}
		} else {
			float step = speed * Time.deltaTime;
			this.transform.position = Vector2.MoveTowards(this.transform.position, GumObject.transform.position, step);
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
			Debug.Log("following Gum");
			if(GumObject.tag == "Gum"){
				GumObject = null;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Monster"){
			monster = other.transform.gameObject;
			other.GetComponent<gravityMonster>().talkedToPlayer = false;
			other.GetComponent<gravityMonster>().blocked = true;
		}
		//if(other.tag == "HoldingGum"){
			//Debug.Log("There's gum");
			//GumObject = other.transform.gameObject;
		//}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "HoldingGum"){
			GumObject = null;
		}
	}
}
