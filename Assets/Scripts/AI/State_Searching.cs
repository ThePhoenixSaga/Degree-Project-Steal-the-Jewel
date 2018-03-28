using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Searching : NPC_FSM_Controller
{
    public float timerLeft; //How many seconds
    public bool searching = false;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timerLeft = 3.0f; //3 seconds for testing, offical timer should be 20 seconds or less (need to experiment)
        searching = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        

        if (searching == true)
        {
            //Go to objective
            agent.SetDestination(objective.transform.position);
            if (Vector3.Distance(objective.transform.position, NPC.transform.position) < accuracy)
            {
                //Wait at objective for a few seconds, maybe spin around to scan for player
                timerLeft -= Time.deltaTime;
                if (timerLeft <= 0)
                {
                    searching = false; //Stop searching and go back to patrol
                    //anim.SetBool("animPlayerNearby", false);
                }
            }
        }



        //Return to first patrol point
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    
    }

}
