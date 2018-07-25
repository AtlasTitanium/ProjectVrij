using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMonster : MonoBehaviour {
	public Animator SceneMonsterAnimator;
	public int WichMonster;
	
	void Start(){
		SceneMonsterAnimator.SetInteger("Monster",WichMonster);
	}
}
