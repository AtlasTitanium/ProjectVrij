using System.Collections;
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
	public GameObject Particles;
	public GameObject leftBarrier,rightBarrier,upBarrier,downBarrier;
	public bool UpScene = false;

	void Start(){
		anim = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer>().sprite = scaredSprite;
		//anim.SetBool("Scared",true);
		//anim.SetBool("Movin",false);
	}
	void Update () {
		if(UpScene){
			GetComponent<Collider2D>().enabled = false;
			if(!waitForNextFrame){
				i += 1;
				if(i == walkSprites.Length){
					i = 0;
				}
				this.GetComponent<SpriteRenderer>().sprite = walkSprites[i];
				StartCoroutine(WaitMonster());
				waitForNextFrame = true;
			}
			transform.position = new Vector2(transform.position.x, transform.position.y + 0.025f);
			return;
		}
		if(leftBarrier != null && rightBarrier != null && upBarrier != null && downBarrier != null ){
			leftBarrier.SetActive(true);
			rightBarrier.SetActive(true);
			upBarrier.SetActive(true);
			downBarrier.SetActive(true);
			if(this.GetComponent<SpriteRenderer>().flipX == false){
				leftBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(-10.44f,0.13f);
				rightBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(0.26f,0.56f);

				upBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(-4.32f,2f);
				downBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(-4.62f,-3.45f);
			} else {
				leftBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(-0.3f,0.13f);
				rightBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(10.6f,0.56f);

				upBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(5.5f,2f);
				downBarrier.GetComponent<BoxCollider2D>().transform.localPosition = new Vector2(4.66f,-3.45f);
			}
		}
		if(blocked){
			//this.GetComponent<SpriteRenderer>().flipX = true;
			GumObject = null;
			this.GetComponent<SpriteRenderer>().sprite = scaredSprite;
			if(this.GetComponent<SpriteRenderer>().flipX == false){
				this.GetComponent<BoxCollider2D>().offset = new Vector2(-1.5f,0.3f);
			} else {
				this.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f,0.3f);
			}
			this.GetComponent<BoxCollider2D>().size = new Vector2(3.7f,3f);
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
				if((GumObject.transform.position.x - this.transform.position.x) < -0.5f){
					if(transform.GetChild(transform.childCount-1).tag == "MonsterBaby"){
						transform.GetChild(transform.childCount-1).transform.localPosition = new Vector3(2.6f,1.2f,-1.1f);
						transform.GetChild(transform.childCount-1).transform.GetComponent<SpriteRenderer>().flipX = false;
					}
					this.GetComponent<SpriteRenderer>().flipX = true;
					Particles.transform.localPosition = new Vector2(-4,-0.5f);
					this.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f,-0.5f);
					this.GetComponent<BoxCollider2D>().size = new Vector2(1.5f,3f);
				}
				if((GumObject.transform.position.x - this.transform.position.x) > -0.5f){
					if(transform.GetChild(transform.childCount-1).tag == "MonsterBaby"){
						transform.GetChild(transform.childCount-1).transform.localPosition = new Vector3(-2f,0.9f,-1.1f);
						transform.GetChild(transform.childCount-1).transform.GetComponent<SpriteRenderer>().flipX = true;
					}
					this.GetComponent<SpriteRenderer>().flipX = false;
					Particles.transform.localPosition = new Vector2(-12,-0.5f);
					this.GetComponent<BoxCollider2D>().offset = new Vector2(-1.5f,-0.5f);
					this.GetComponent<BoxCollider2D>().size = new Vector2(3.7f,3f);
				}
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
				//anim.SetBool("Movin",true);
				//Debug.Log("following Gum");
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
	

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Crate"){
			Debug.Log(other.gameObject);
			this.GetComponent<SpriteRenderer>().flipX = true;
			blocked = true;
		}
	}
	*/

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Crate"){
			this.GetComponent<SpriteRenderer>().flipX = false;
			this.GetComponent<SpriteRenderer>().sprite = idleSprite;
			this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.2f,1.8f);
				this.GetComponent<BoxCollider2D>().size = new Vector2(4.15f,5.65f);
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
