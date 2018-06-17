using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gravityMonster : MonoBehaviour {
	public Image ShadowText;
	public Text UnderText;
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
			this.GetComponent<SpriteRenderer>().sprite = scaredSprite;
			UnderText.text = "Help Me...";
			if(transparesy > 0.5f){
				return;
			}
			if(!howfast){
				ShadowText.color = new Color(ShadowText.color.r,ShadowText.color.g,ShadowText.color.b,transparesy);
				UnderText.color = new Color(UnderText.color.r,UnderText.color.g,UnderText.color.b,transparesy*2);
				transparesy += 0.02f;
				StartCoroutine(Fast());
			}
			return;
		} else {
			Debug.Log("not Blocked");
			if(transparesy == 0.0f){
				howfast = true;
			}
			if(!howfast){
				ShadowText.color = new Color(ShadowText.color.r,ShadowText.color.g,ShadowText.color.b,transparesy);
				UnderText.color = new Color(UnderText.color.r,UnderText.color.g,UnderText.color.b,transparesy*2);
				transparesy -= 0.02f;
				StartCoroutine(Fast());
			}
		}
		if(talkedToPlayer){
			if(GumObject == null){
				this.GetComponent<SpriteRenderer>().sprite = idleSprite;
				//anim.SetBool("Movin",false);
				GumObject = GameObject.FindGameObjectWithTag("HoldingGum");
			} else {
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
				Debug.Log("following Gum");
				if(GumObject.tag == "Gum"){
					this.GetComponent<SpriteRenderer>().sprite = idleSprite;
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
}
