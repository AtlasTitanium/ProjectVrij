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
	private FieldOfVieuw fow;
	public Sprite[] walkSprites;
	public Sprite idleSprite;
	[HideInInspector]
	public bool wet = false;
	private int i = 0;

	void Start(){
		fow = GetComponent<FieldOfVieuw>();
	}

	void Update () {
		for(int i = 0; i < fow.visibleTargets.Count; i++){
			if(fow.visibleTargets[i].tag == "Monster"){
				monster = fow.visibleTargets[i].gameObject;
			}
			if(fow.visibleTargets[i].tag == "Gum"){
				GumObject = fow.visibleTargets[i].gameObject;
			}
		}
		if(fow.visibleTargets.Count == 0){
			GumObject = null;
			monster = null;
		}
		if(!wet){
			if(GumObject != null){
				if(!waitForNextFrame){
					i += 1;
					if(i == walkSprites.Length){
						i = 0;
					}
					this.GetComponent<SpriteRenderer>().sprite = walkSprites[i];
					StartCoroutine(WaitMonster());
					waitForNextFrame = true;
				}
				float step = speed * Time.deltaTime;
				this.transform.position = Vector2.MoveTowards(this.transform.position, GumObject.transform.position, step);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
				Debug.Log("following Gum");
				if(GumObject.tag == "Gum"){
					GumObject = null;
				}
				return;
			}
		}
		if(monster != null){
			if(!waitForNextFrame){
				i += 1;
				if(i == walkSprites.Length){
					i = 0;
				}
				this.GetComponent<SpriteRenderer>().sprite = walkSprites[i];
				StartCoroutine(WaitMonster());
				waitForNextFrame = true;
			}
			float stepo = speed * Time.deltaTime;
			this.transform.position = Vector2.MoveTowards(this.transform.position, monster.transform.position, stepo);
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
			Debug.Log("following monster");
			//return;
		}
	}

	public void Dry(){
		StartCoroutine(DryUp());
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Monster"){
			Application.LoadLevel(4);
		}
	}

	IEnumerator WaitMonster()
    {
        yield return new WaitForSeconds(0.2f);
        waitForNextFrame = false;
    }
	IEnumerator DryUp()
    {
        yield return new WaitForSeconds(2.0f);
        wet = false;
    }
}
