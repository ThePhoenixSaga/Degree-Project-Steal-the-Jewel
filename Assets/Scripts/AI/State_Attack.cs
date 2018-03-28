using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Attack : NPC_FSM_Controller {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        audioSource.PlayOneShot(soundAttack);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (audioRepeatTime > 10.0f)
        {
            if (Time.time >= finishTime)
            {
                finishTime = Time.time + soundAttack.length;
                audioSource.PlayOneShot(soundAttack);
            }
            audioRepeatTime = 0f;
        }
        audioRepeatTime++;

        agent.SetDestination(player.transform.position);
        Shoot();
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

    void Shoot()
    {
        if (bullet == null)//Shoot one bullet at a time
        {
            //Get rotation of turret
            Vector3 relativePos = player.transform.position - turret.transform.position;
            Quaternion newTurretRotation = Quaternion.LookRotation(relativePos);

            //Grab bullet prefab from assests folder
            bullet = Resources.Load("Bullet") as GameObject;

            //Create bullet from prefab
            var bulletClone = (GameObject)Instantiate(bullet, turret.transform.position, newTurretRotation);

            //Destroy bullet after a few seconds
            Destroy(bulletClone, 2.0f);

        }
        else
        {
            //Stops repeats when switching states
        }

    }

}
