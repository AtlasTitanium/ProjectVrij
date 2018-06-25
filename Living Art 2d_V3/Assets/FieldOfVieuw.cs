using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfVieuw : MonoBehaviour {

	public float viewRadius;
	public LayerMask targetmask;
	public LayerMask obstacles;

	public List<Transform> visibleTargets = new List<Transform>();
	public Collider2D[] targetsInViewRadius = new Collider2D[25];

	void Start(){
		StartCoroutine("FindTargetsWithDelay" , 0.2f);
	}

	IEnumerator FindTargetsWithDelay(float delay){
		while(true){
			yield return new WaitForSeconds (delay);
			FindVisibleTargets();
		}
	}
	void FindVisibleTargets(){
		//Debug.Log("checking");
		visibleTargets.Clear();
		for(int i = 0; i < targetsInViewRadius.Length; i++){
			targetsInViewRadius[i] = null;
		}
		if(Physics2D.OverlapCircleNonAlloc(transform.position, viewRadius, targetsInViewRadius, targetmask) > 0){
			for(int i = 0; i < targetsInViewRadius.Length; i++){
				if(targetsInViewRadius[i] != null){
					Transform target = targetsInViewRadius[i].transform;
					Vector2 dirToTarget = target.position - transform.position;
					float dstToTarget = dirToTarget.magnitude;
					if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacles)){
						//Debug.Log("Monster!");
						visibleTargets.Add(target);
					}
				}	
			}
		}
	}
}
