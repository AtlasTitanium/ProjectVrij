using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveImage : MonoBehaviour {
	public float xMoveSpeed;
	public float yMoveSpeed;
	private float xPos;
	private float yPos;
	void Start () {
		xPos = this.transform.position.x;
		yPos = this.transform.position.y;
	}
	
	void Update () {
		this.transform.position = new Vector2(xPos,yPos);
		xPos += xMoveSpeed * Time.deltaTime;
		yPos += yMoveSpeed * Time.deltaTime;
	}
}
