using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Chase : NPC_FSM_Controller {

    public Vector3 oldPlayerPosition;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        oldPlayerPosition = player.transform.position;
        audioSource.PlayOneShot(soundChase);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (audioRepeatTime > 10.0f)
        {
            if (Time.time >= finishTime)
            {
                finishTime = Time.time + soundChase.length;
                audioSource.PlayOneShot(soundChase);
            }
            audioRepeatTime = 0f;
        }
        audioRepeatTime++;

        agent.SetDestination(player.transform.position);

        if (oldPlayerPosition == player.transform.position)
        {
            //Debug.Log("Player is out of view");
        }
        //Debug.Log(oldPlayerPosition.ToString("F4"));
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    
	}

}
