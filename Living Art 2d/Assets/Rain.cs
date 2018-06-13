using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour{
	ParticleSystem ps;

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
		
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
			Debug.Log("enter monster");
			col.gameObject.GetComponent<gravityMonster>().blocked = true;
			col.gameObject.transform.localPosition = new Vector2(col.gameObject.transform.localPosition.x-0.5f,col.gameObject.transform.localPosition.y);
            col.gameObject.GetComponent<gravityMonster>().anim.SetBool("Scared",true);
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
			Debug.Log("leave monster");
			col.gameObject.GetComponent<gravityMonster>().blocked = false;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
		
    }
}
