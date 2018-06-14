using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumPlace : MonoBehaviour {
	public GameObject gumPrefab;
	public GameObject gum;
	private Color originalColor;
	public GameObject otherGumPlace;
	public GameObject monster;
	private bool follow = true;
	public Sprite middlegumSprite;
	public Sprite endgumSprite;
	void Update () {
		if(gum == null){
			if(Input.GetKeyUp("r")){
				gum = Instantiate(gumPrefab, this.transform.position, this.transform.rotation);
				gum.transform.parent = this.transform;
			} 
		} else {
			if(follow){
				gum.transform.position = this.transform.position;
			}
			if(Input.GetKeyDown("r")){
				gum.transform.parent = null;
				gum.GetComponent<Rigidbody2D>().gravityScale = 1;
				gum.GetComponent<Collider2D>().isTrigger = false;
				follow = false;
				StartCoroutine(WaitForGum());
			} 
			if(otherGumPlace != null){
				if(Input.GetMouseButtonDown(0)){
					//Debug.Log("Place gum");
					gum.tag = "Gum";
					gum.GetComponent<SpriteRenderer>().sprite = middlegumSprite;
					StartCoroutine(NextGumSprite(gum));
					gum.transform.parent = otherGumPlace.transform;
					gum.transform.position = otherGumPlace.transform.position;
					gum.transform.localPosition = new Vector3(gum.transform.localPosition.x,gum.transform.localPosition.y,-1);
					gum.transform.localScale = new Vector2(gum.transform.localScale.x * 2,gum.transform.localScale.y * 2);
					gum.transform.rotation = Quaternion.identity;
					otherGumPlace.GetComponent<AGuMPLace>().hasGum = true;
					otherGumPlace.GetComponent<SpriteRenderer>().color = originalColor;
					gum = null;
				}
			}
			if(monster != null){
				if(Input.GetMouseButtonDown(0)){
					//Debug.Log("Place gum");
					monster.GetComponent<gravityMonster>().talkedToPlayer = true;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.transform.tag == "AGumPlace"){
			otherGumPlace = other.gameObject;
			originalColor = other.GetComponent<SpriteRenderer>().color;
			if(!otherGumPlace.GetComponent<AGuMPLace>().hasGum){
				other.GetComponent<SpriteRenderer>().color = Color.yellow;
			}
		}
		if(other.transform.tag == "Monster"){
			monster = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.transform.tag == "AGumPlace"){
			other.GetComponent<SpriteRenderer>().color = originalColor;
			otherGumPlace = null;
		}
		if(other.transform.tag == "Monster"){
			if(monster != null){
				//monster.GetComponent<gravityMonster>().talkedToPlayer = false;
				monster = null;
			}
		}
	}

	IEnumerator WaitForGum()
    {
        yield return new WaitForSeconds(2);
        Destroy(gum);
		follow = true;
		if(monster != null){
			monster.GetComponent<gravityMonster>().talkedToPlayer = false;
			monster.GetComponent<SpriteRenderer>().sprite = monster.GetComponent<gravityMonster>().idleSprite;
		}
		//monster = null;
    }

	IEnumerator NextGumSprite(GameObject gum)
    {
        yield return new WaitForSeconds(0.5f);
		Debug.Log(gum);
        gum.GetComponent<SpriteRenderer>().sprite = endgumSprite;
    }
}
