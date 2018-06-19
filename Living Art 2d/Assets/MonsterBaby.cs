using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBaby : MonoBehaviour {

	public Image ShadowText;
	public Text UnderText;
	public bool blocked = false;
	public GameObject GumObject;
	public float speed;
	public bool talkedToPlayer = false;
	private float transparesy = 0.1f;
	private bool howfast = false;

	public Sprite[] idleSprites;

	private bool waitForNextFrame = false;
	private int i = -1;

	void Update () {
		if(!waitForNextFrame){
			i += 1;
			if(i == idleSprites.Length){
				i = 0;
			}
			this.GetComponent<SpriteRenderer>().sprite = idleSprites[i];
			StartCoroutine(WaitMonster());
			Debug.Log("SpriteChange");
			waitForNextFrame = true;
		}
		if(talkedToPlayer){
			if(GumObject == null){
				//anim.SetBool("Movin",false);
				GumObject = GameObject.FindGameObjectWithTag("HoldingGum");
			} else {
				float step = speed * Time.deltaTime;
				this.transform.position = Vector2.MoveTowards(this.transform.position, GumObject.transform.position, step);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
				//anim.SetBool("Movin",true);
				Debug.Log("following Gum");
				if(GumObject.tag == "Gum"){
					//anim.SetBool("Movin",false);
					GumObject = null;
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
}
