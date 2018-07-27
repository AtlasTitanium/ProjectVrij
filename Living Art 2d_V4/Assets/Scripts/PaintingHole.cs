using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingHole : MonoBehaviour {
	public bool hasGum = false;
	public GameObject filler;
	public void FillHole(){
		filler.SetActive(true);
	}
}
