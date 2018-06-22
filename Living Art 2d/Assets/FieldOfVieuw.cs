using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfVieuw : MonoBehaviour {

	public float viewRadius;
	public LayerMask targetmask;
	public LayerMask obstacles;

	public List<Transform> visibleTargets = new List<Transform>();

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
		Collider2D targetsInViewRadius = Physics2D.OverlapCircle(transform.position, viewRadius, targetmask);
		if(targetsInViewRadius != null){
			Transform target = targetsInViewRadius.transform;
			Vector2 dirToTarget = (target.position - transform.position).normalized;
			float dstToTarget = Vector2.Distance (transform.position, target.position);
			if(!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, obstacles)){
				//Debug.Log("Monster!");
				visibleTargets.Add(target);
			}
		}	
	}
}
