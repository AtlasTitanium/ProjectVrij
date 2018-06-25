﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gravityMonster : MonoBehaviour {
	public bool blocked = false;
	public GameObject GumObject;
	public float speed;
	public bool talkedToPlayer = false;
	private float transparesy = 0.1f;
	private bool howfast = false;
	[HideInInspector]
	public Animator anim;
	public Sprite[] walkSprites;
	public Sprite idleSprite;
	public Sprite scaredSprite;
	private bool waitForNextFrame = false;
	private int i = -1;
	void Start(){
		anim = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer>().sprite = scaredSprite;
		//anim.SetBool("Scared",true);
		//anim.SetBool("Movin",false);
	}
	void Update () {
		if(blocked){
			//this.GetComponent<SpriteRenderer>().flipX = true;
			GumObject = null;
			this.GetComponent<SpriteRenderer>().sprite = scaredSprite;
			this.GetComponent<BoxCollider2D>().offset = new Vector2(-4.0f,0.1f);
			this.GetComponent<BoxCollider2D>().size = new Vector2(8.5f,3f);
			if(transparesy > 0.5f){
				return;
			}
			if(!howfast){
				transparesy += 0.02f;
				StartCoroutine(Fast());
			}
			return;
		} else {
			this.GetComponent<SpriteRenderer>().flipX = false;
			//Debug.Log("not Blocked");
			if(transparesy == 0.0f){
				howfast = true;
			}
			if(!howfast){
				transparesy -= 0.02f;
				StartCoroutine(Fast());
			}
		}
		if(talkedToPlayer){
			if(GumObject == null){
				this.GetComponent<SpriteRenderer>().flipX = false;
				this.GetComponent<SpriteRenderer>().sprite = idleSprite;
				this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.2f,1.8f);
				this.GetComponent<BoxCollider2D>().size = new Vector2(4.15f,5.65f);
				//anim.SetBool("Movin",false);
				GumObject = GameObject.FindGameObjectWithTag("HoldingGum");
				StartCoroutine(WaitForOff());
			} else {
				if(!waitForNextFrame){
					i += 1;
					if(i == walkSprites.Length){
						i = 0;
					}
					this.GetComponent<SpriteRenderer>().sprite = walkSprites[i];
					this.GetComponent<BoxCollider2D>().offset = new Vector2(-3.85f,-0.7f);
					this.GetComponent<BoxCollider2D>().size = new Vector2(8,3.22f);
					StartCoroutine(WaitMonster());
					waitForNextFrame = true;
				}
				float step = speed * Time.deltaTime;
				this.transform.position = Vector2.MoveTowards(this.transform.position, GumObject.transform.position, step);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
				//anim.SetBool("Movin",true);
				Debug.Log("following Gum");
				if(GumObject.tag == "Gum"){
					this.GetComponent<SpriteRenderer>().sprite = idleSprite;
					//anim.SetBool("Movin",false);
					//GumObject = null;
				}
			}
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "SpiderMonster"){
			Debug.Log("There's a monster");
			other.GetComponent<SpiderMonster>().monster = this.transform.gameObject;
			blocked = true;
			talkedToPlayer = false;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Crate"){
			this.GetComponent<SpriteRenderer>().sprite = idleSprite;
			//anim.SetBool("Scared",false);
			blocked = false;
		}
		if(other.tag == "SpiderMonster"){
			Debug.Log("There's a monster");
			other.GetComponent<SpiderMonster>().monster = null;
			blocked = false;
		}
	}
	*/

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Crate"){
			this.GetComponent<SpriteRenderer>().sprite = idleSprite;
			//anim.SetBool("Scared",false);
			blocked = false;
		}
	}

	IEnumerator Fast()
    {
        howfast = true;
        yield return new WaitForSeconds(0.05f);
        howfast = false;
    }

	IEnumerator WaitMonster()
    {
        yield return new WaitForSeconds(0.2f);
        waitForNextFrame = false;
    }
	IEnumerator WaitForOff()
    {
        yield return new WaitForSeconds(0.5f);
       	if(GumObject == null){
        	talkedToPlayer = false;
		}
    }
}