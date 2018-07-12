using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground2 : MonoBehaviour {

	[SerializeField]GameObject[] scrollables;
	Vector2 curpos;
	Vector2 lastFramePos;

	void Start () {
		lastFramePos = new Vector2(this.transform.position.x,this.transform.position.y);
	}
	

	void Update () {
		curpos = new Vector2(this.transform.position.x,this.transform.position.y);
		if(lastFramePos == curpos){return;}
		
		float cal = 1.1f;
		int off = 2;
		
		foreach(GameObject s in scrollables){
			s.transform.position = new Vector3(s.transform.position.x,s.transform.position.y - (curpos.y - lastFramePos.y)*cal, -off);
			cal += 0.1f;
			off += 2;
		}
		lastFramePos = curpos;
	}
}
