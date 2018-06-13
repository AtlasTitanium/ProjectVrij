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
	void Start(){
		blocked = true;
	}
	void Update () {
		if(blocked){
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
				GumObject = GameObject.FindGameObjectWithTag("HoldingGum");
			} else {
				float step = speed * Time.deltaTime;
				this.transform.position = Vector2.MoveTowards(this.transform.position, GumObject.transform.position, step);
				this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,1);
				if(GumObject.tag == "Gum"){
					GumObject = null;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.transform.tag == "Crate"){
			blocked = false;
		}
	}

	IEnumerator Fast()
    {
        howfast = true;
        yield return new WaitForSeconds(0.05f);
        howfast = false;
    }
}
