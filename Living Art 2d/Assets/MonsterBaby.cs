using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBaby : MonoBehaviour {
	public bool blocked = false;
	public GameObject GumObject;
	public float speed;
	public bool talkedToPlayer = false;
	public Sprite[] idleSprites;
	private bool waitForNextFrame = false;
	private int i = -1;
	private GameObject parent;

	void Update () {
		if(!waitForNextFrame){
			i += 1;
			if(i == idleSprites.Length){
				i = 0;
			}
			this.GetComponent<SpriteRenderer>().sprite = idleSprites[i];
			StartCoroutine(WaitMonster());
			//Debug.Log("SpriteChange");
			waitForNextFrame = true;
		}
		if(talkedToPlayer){
			if(GumObject == null){
				//anim.SetBool("Movin",false);
				GumObject = GameObject.FindGameObjectWithTag("HoldingGum");
				StartCoroutine(WaitForOff());
			} else {
				float step = speed * Time.deltaTime;
				this.transform.position = Vector2.MoveTowards(this.transform.position, GumObject.transform.position, step);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
				//anim.SetBool("Movin",true);
				Debug.Log("following Gum");
				if(GumObject.tag == "Gum"){
					//anim.SetBool("Movin",false);
					GumObject = null;
					talkedToPlayer = false;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "SpiderMonster"){
			Debug.Log("There's a monster");
			other.GetComponent<SpiderMonster>().monster = this.transform.gameObject;
			blocked = true;
			talkedToPlayer = false;
		}
		if(other.tag == "Monster"){
			parent = other.gameObject;
			StartCoroutine(WaitForMonster());
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "SpiderMonster"){
			Debug.Log("There's a monster");
			other.GetComponent<SpiderMonster>().monster = null;
			blocked = false;
		}
	}

	IEnumerator WaitMonster()
    {
        yield return new WaitForSeconds(0.4f);
        waitForNextFrame = false;
    }

	IEnumerator WaitForMonster()
    {
        yield return new WaitForSeconds(2f);
        this.transform.parent = parent.transform;
		this.transform.localPosition = new Vector3(-2f,0.9f,-0.3f);
		this.GetComponent<SpriteRenderer>().flipX = enabled;
		this.enabled = false;
    }
	IEnumerator WaitForOff()
    {
        yield return new WaitForSeconds(0.5f);
		if(GumObject == null){
        	talkedToPlayer = false;
		}
    }
}
