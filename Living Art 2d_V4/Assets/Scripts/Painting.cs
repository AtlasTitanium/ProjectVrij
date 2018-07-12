using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour {

	public GameObject[] holes;
	private GameObject LeftSpot, RightSpot;
	public MouseText mouse;
	void Update(){
		bool holesfilled = true;
		for(int i=0; i < holes.Length; i++)
		{
			if (holes[i].GetComponent<PaintingHole>().filler.active == false)
			{
				holesfilled = false;
				break;
			}
		}
		if(holesfilled){
			this.gameObject.tag = "Wall";
			this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
		}
	}
}
