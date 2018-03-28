using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class State_Patrol : NPC_FSM_Controller {

    public int destPoints = 0;
    public GameObject[] waypoints;

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").OrderBy(go => go.name).ToArray(); //Gets waypoint gameobject and sorts path objects in alphabetic order.
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        audioSource.PlayOneShot(soundPatrol);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (audioRepeatTime > 10.0f)
        {
            if (Time.time >= finishTime)
            {
                finishTime = Time.time + soundPatrol.length;
                audioSource.PlayOneShot(soundPatrol);
            }
            audioRepeatTime = 0f;
        }
        audioRepeatTime++;


        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        if (Vector3.Distance(waypoints[destPoints].transform.position, NPC.transform.position) < accuracy)
        {
            destPoints++;
            if (destPoints >= waypoints.Length)
            {
                destPoints = 0;
            }
        }

        agent.SetDestination(waypoints[destPoints].transform.position);

        //Debug.Log("Point count is at: "+ destPoints);
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    
	}

}
