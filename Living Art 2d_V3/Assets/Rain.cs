using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour{
	ParticleSystem ps;
    private GameObject Spoodmonst;
    private GameObject monst;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger(){
        // get the particles which matched the trigger conditions this frame
		var col = ps.trigger.GetCollider(0);
		//Debug.Log(col.gameObject);
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        if(col.gameObject.tag == "Monster"){
             // iterate through the particles which entered the trigger and make them red
            for (int i = 0; i < numEnter; i++)
            {
                monst = col.gameObject;
                Debug.Log("enter monster");
                monst.GetComponent<gravityMonster>().blocked = true;
                monst.GetComponent<gravityMonster>().talkedToPlayer = false;
                monst.transform.localPosition = new Vector2(monst.transform.localPosition.x-0.5f,monst.transform.localPosition.y);
                monst.GetComponent<gravityMonster>().anim.SetBool("Scared",true);
                StartCoroutine(FastM());
            }
        }
        if(col.gameObject.tag == "SpiderMonster"){
             // iterate through the particles which entered the trigger and make them red
            for (int i = 0; i < numEnter; i++)
            {
                Spoodmonst = col.gameObject;
                Debug.Log("enter Spooder");
                Spoodmonst.GetComponent<SpiderMonster>().GumObject = null;
                Spoodmonst.GetComponent<Collider2D>().enabled = false;
                Spoodmonst.transform.localPosition = new Vector2(Spoodmonst.transform.localPosition.x+0.8f,Spoodmonst.transform.localPosition.y);
                StartCoroutine(Fast());
            }
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		
    }

    IEnumerator Fast()
    {
        yield return new WaitForSeconds(0.5f);
        Spoodmonst.gameObject.GetComponent<Collider2D>().enabled = true;
        Spoodmonst = null;
    }

    IEnumerator FastM()
    {
        yield return new WaitForSeconds(1f);
        monst.GetComponent<gravityMonster>().blocked = false;
        monst = null;
    }
}
