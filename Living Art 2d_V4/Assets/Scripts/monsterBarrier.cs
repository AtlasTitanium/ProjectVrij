using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBarrier : MonoBehaviour {

	public GameObject TheCreature;
	public bool Right;
	public bool Left;
	public bool Up;
	public bool Down;
	public GameObject wall;
	public GameObject outside;

	void Update(){
		if(wall == null){
			if(outside != null){
				if(Right){
					TheCreature.transform.position = new Vector2(TheCreature.transform.position.x - 1,TheCreature.transform.position.y);
					TheCreature.GetComponent<gravityMonster>().blocked = true;
					StartCoroutine(notBlocked());
				}
				if(Left){
					TheCreature.transform.position = new Vector2(TheCreature.transform.position.x + 1,TheCreature.transform.position.y);
					TheCreature.GetComponent<gravityMonster>().blocked = true;
					StartCoroutine(notBlocked());
				}
				if(Up){
					TheCreature.transform.position = new Vector2(TheCreature.transform.position.x,TheCreature.transform.position.y - 1);
					TheCreature.GetComponent<gravityMonster>().blocked = true;
					StartCoroutine(notBlocked());
				}
				if(Down){
					TheCreature.transform.position = new Vector2(TheCreature.transform.position.x,TheCreature.transform.position.y + 1);
					TheCreature.GetComponent<gravityMonster>().blocked = true;
					StartCoroutine(notBlocked());
				}
				outside = null;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Outside"){
			outside = other.gameObject;
		}
		if(other.tag == "Wall"){
			outside = null;
			wall = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Wall"){
			wall = null;
		}
	}
	IEnumerator notBlocked()
    {
        yield return new WaitForSeconds(1f);
        TheCreature.GetComponent<gravityMonster>().blocked = false;
    }
}
