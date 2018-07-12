using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinePlace : MonoBehaviour {
	public GameObject Trampoline;
	public GameObject Gum;
	public bool hasGum;
	public void CREATE(){
		Destroy(Gum);
		Gum = null;
		Trampoline.SetActive(true);
		Destroy(this.gameObject);
	}
}
