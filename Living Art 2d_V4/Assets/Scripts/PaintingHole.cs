using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingHole : MonoBehaviour {

	public GameObject filler;
	public void FillHole(){
		filler.SetActive(true);
	}
}
