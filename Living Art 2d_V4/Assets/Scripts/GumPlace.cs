using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumPlace : MonoBehaviour {
	public GameObject gumPrefab;
	public GameObject gum;
	private Color originalColor;
	public GameObject otherGumPlace;
	public GameObject trampoline;
	public GameObject monster;
	public GameObject Babymonster;
	private bool follow = true;
	public Sprite middlegumSprite;
	public Sprite endgumSprite;
	public MouseText Mouse;
	public bool TPintheMaking = false;
	private float XscaleOrigin;
	private float XactualscaleOrigin;
	private float angle;
	private Vector3 dir;
	void Update () {
		if(TPintheMaking){
			//set size
			trampoline.GetComponent<TrampolinePlace>().Gum.transform.localScale = new Vector2(XactualscaleOrigin,trampoline.GetComponent<TrampolinePlace>().Gum.transform.localScale.y);
			XscaleOrigin = trampoline.GetComponent<TrampolinePlace>().Gum.transform.localScale.x * Mathf.Sqrt(((gum.transform.position.x - trampoline.transform.position.x)*(gum.transform.position.x - trampoline.transform.position.x) + (gum.transform.position.y - trampoline.transform.position.y)*(gum.transform.position.y - trampoline.transform.position.y)));

			//change pos
			gum.transform.position = this.transform.position;
			trampoline.GetComponent<TrampolinePlace>().Gum.transform.position = new Vector2(CalculatePosition(trampoline.transform.position.x, gum.transform.position.x),CalculatePosition(trampoline.transform.position.y, gum.transform.position.y));
			//change size
			trampoline.GetComponent<TrampolinePlace>().Gum.transform.localScale = new Vector2(XscaleOrigin * 2,trampoline.GetComponent<TrampolinePlace>().Gum.transform.localScale.y);
			//change rotation
			dir = gum.transform.position - trampoline.transform.position;
			angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			trampoline.GetComponent<TrampolinePlace>().Gum.transform.rotation = Quaternion.AngleAxis(angle/*Doe hier -20 om de angle te veranderen*/, Vector3.forward);
			
			if(Input.GetButtonDown("Fire3")){
				Destroy(gum);
				gum = null;
				trampoline.GetComponent<TrampolinePlace>().CREATE();
				trampoline.GetComponent<TrampolinePlace>().enabled = false;
				TPintheMaking = false;
			}
		} else {
			if(gum == null){
				if(Input.GetButtonDown("Fire2")){
					gum = Instantiate(gumPrefab, this.transform.position, this.transform.rotation);
					gum.transform.parent = this.transform;
				} 
			} else {
				if(follow){
					gum.transform.position = this.transform.position;
				}
				if(otherGumPlace != null){
					if(otherGumPlace.GetComponent<AGuMPLace>().hasGum != true){
						if(otherGumPlace.GetComponent<AGuMPLace>().ShowText){
							Mouse.DisplayText("Press Mouse 1 to place gum");
						}
					}
					if(Input.GetButtonDown("Fire3")){
						Mouse.DisplayText("");
						//Debug.Log("Place gum");
						gum.tag = "Gum";
						gum.GetComponent<SpriteRenderer>().sprite = middlegumSprite;
						StartCoroutine(NextGumSprite(gum));
						gum.transform.parent = otherGumPlace.transform;
						gum.GetComponent<Rigidbody2D>().gravityScale = 0;
						gum.transform.position = otherGumPlace.transform.position;
						gum.transform.localPosition = new Vector3(gum.transform.localPosition.x,gum.transform.localPosition.y,-1);
						gum.transform.localScale = new Vector2(gum.transform.localScale.x * 2,gum.transform.localScale.y * 2);
						gum.transform.rotation = Quaternion.identity;
						otherGumPlace.GetComponent<AGuMPLace>().hasGum = true;
						//otherGumPlace.GetComponent<SpriteRenderer>().color = originalColor;
						gum = null;
					}
				} else {
					if(Input.GetButtonDown("Fire2")){
						gum.transform.parent = null;
						gum.GetComponent<Rigidbody2D>().gravityScale = 1;
						gum.GetComponent<Collider2D>().isTrigger = false;
						follow = false;
						StartCoroutine(WaitForGum());
					} 
				}
				if(trampoline != null){
					if(trampoline.GetComponent<TrampolinePlace>().hasGum != true){
						if(trampoline.GetComponent<TrampolinePlace>().ShowText){
							Mouse.DisplayText("Press Mouse 1 to place gum");
						}
						if(Input.GetButtonDown("Fire3")){
							Mouse.DisplayText("");
							//Debug.Log("Place gum");
							gum.tag = "Gum";
							gum.GetComponent<SpriteRenderer>().sprite = middlegumSprite;
							gum.transform.parent = null;;
							gum.GetComponent<Rigidbody2D>().gravityScale = 0;
							gum.transform.position = trampoline.transform.position;
							//gum.transform.localPosition = new Vector3(gum.transform.localPosition.x,gum.transform.localPosition.y,-1);
							gum.transform.localScale = new Vector2(gum.transform.localScale.x * 2,gum.transform.localScale.y * 2);
							gum.transform.rotation = Quaternion.identity;
							trampoline.GetComponent<TrampolinePlace>().hasGum = true;
							trampoline.GetComponent<TrampolinePlace>().Gum = gum;
							gum = null;
						}
					} else {
						if(trampoline.GetComponent<TrampolinePlace>().ShowText){
							Mouse.DisplayText("Press Mouse 1 to make trampoline");
						}
						if(!TPintheMaking){
							if(Input.GetButtonDown("Fire3")){
								XscaleOrigin = trampoline.GetComponent<TrampolinePlace>().Gum.transform.localScale.x * Mathf.Sqrt(((gum.transform.position.x - trampoline.transform.position.x)*(gum.transform.position.x - trampoline.transform.position.x) + (gum.transform.position.y - trampoline.transform.position.y)*(gum.transform.position.y - trampoline.transform.position.y)));
								XactualscaleOrigin = trampoline.GetComponent<TrampolinePlace>().Gum.transform.localScale.x;
								Mouse.DisplayText("");
								TPintheMaking = true;
							}
						}
					}
					
				}
				if(monster != null){
					if(monster.GetComponent<gravityMonster>().talkedToPlayer != true){
						if(monster.GetComponent<gravityMonster>().blocked != true){
							Mouse.DisplayText("Press Left Mouse to lure creature");
							//Debug.Log("There's a monster");
						}
					}
					if(Input.GetButtonDown("Fire3")){
						Mouse.DisplayText("");
						//Debug.Log("Place gum");
						monster.GetComponent<gravityMonster>().talkedToPlayer = true;
					}
				}
				if(Babymonster != null){
					if(Babymonster.GetComponent<MonsterBaby>().talkedToPlayer != true){
						Mouse.DisplayText("Press Left Mouse to lure baby");
						Debug.Log("There's a baby monster");
					}
					
					if(Input.GetButtonDown("Fire3")){
						Mouse.DisplayText("");
						//Debug.Log("Place gum");
						Babymonster.GetComponent<MonsterBaby>().talkedToPlayer = true;
					}
				}
			}
		}
	}

	float CalculatePosition(float guminPlace, float guminHand){
		/*naar de player toe*/ //return ((guminHand - guminPlace)/2)+guminPlace;
		/*van de player weg*/ 	return guminPlace-((guminHand - guminPlace)/2);
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.transform.tag == "AGumPlace"){
			if(gum != null){
				otherGumPlace = other.gameObject;
			}
		}
		if(other.transform.tag == "TrampolinePlace"){
			if(gum != null){
				if(other.GetComponent<TrampolinePlace>().enabled = true){
					trampoline = other.gameObject;
				}	
			}
		}
		if(other.transform.tag == "Monster"){
			if(gum != null){
				monster = other.gameObject;
			}
		}
		if(other.transform.tag == "MonsterBaby"){
			if(gum != null){
				Babymonster = other.gameObject;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.transform.tag == "AGumPlace"){
			//other.GetComponent<SpriteRenderer>().color = originalColor;
			Mouse.DisplayText("");
			otherGumPlace = null;
		}
		if(other.transform.tag == "TrampolinePlace"){
			if(TPintheMaking){
				Mouse.DisplayText("");
			} else {
				Mouse.DisplayText("");
				trampoline = null;
			}
			//other.GetComponent<SpriteRenderer>().color = originalColor;
			
		}
		if(other.transform.tag == "Monster"){
			if(monster != null){
				Mouse.DisplayText("");
				//monster.GetComponent<gravityMonster>().talkedToPlayer = false;
				monster = null;
			}
		}
		if(other.transform.tag == "MonsterBaby"){
			if(Babymonster != null){
				Mouse.DisplayText("");
				//monster.GetComponent<gravityMonster>().talkedToPlayer = false;
				Babymonster = null;
			}
		}
	}

	IEnumerator WaitForGum()
    {
        yield return new WaitForSeconds(2);
		//Debug.Log("begone");
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
		//Debug.Log(gum);
        gum.GetComponent<SpriteRenderer>().sprite = endgumSprite;
    }
}


