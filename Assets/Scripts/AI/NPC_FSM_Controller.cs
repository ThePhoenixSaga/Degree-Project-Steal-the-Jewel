using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_FSM_Controller : StateMachineBehaviour {
    //The main state controller which contains data values of the enemy and the player to be used in the other states

    Animator anim;
    public GameObject NPC;

    public NavMeshAgent agent;
    public GameObject player;
    public GameObject turret;
    public GameObject bullet;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;
    public LineOfSight ThisLineOfSight;

    public GameObject objective;

    public float audioRepeatTime;
    public float finishTime;
    public AudioSource audioSource;
    public AudioClip soundPatrol;
    public AudioClip soundChase;
    public AudioClip soundAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;

        player = NPC.GetComponent<EnemyConstructor>().GetPlayer();
        turret = NPC.GetComponent<EnemyConstructor>().GetTurret();
        bullet = NPC.GetComponent<EnemyConstructor>().GetBullet();
        agent = NPC.GetComponent<NavMeshAgent>();
        ThisLineOfSight = NPC.GetComponent<LineOfSight>();

        objective = NPC.GetComponent<EnemyConstructor>().GetObjective();

        finishTime = Time.time;
        audioSource = NPC.GetComponent<EnemyConstructor>().GetAudioSource();
        soundPatrol = NPC.GetComponent<EnemyConstructor>().GetSoundPatrol();
        soundChase = NPC.GetComponent<EnemyConstructor>().GetSoundChase();
        soundAttack = NPC.GetComponent<EnemyConstructor>().GetSoundAttack();

    }
}
