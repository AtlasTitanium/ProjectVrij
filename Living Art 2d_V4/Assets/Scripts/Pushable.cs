using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

	public Collider2D leftCollider;
	public Collider2D rightCollider;

	public void ChangeColliders(){
		rightCollider.isTrigger = !rightCollider.isTrigger;
		leftCollider.isTrigger = !leftCollider.isTrigger;
	}
}
