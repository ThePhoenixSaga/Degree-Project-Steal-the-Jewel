using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyConstructor : MonoBehaviour
{
    //Contains standard code for moving the enemy around and getting the reference objects like the player
    
    public Animator anim;
    public GameObject player;
    public GameObject bullet;
    public GameObject turret;

    public GameObject objective;

    private LineOfSight ThisLineOfSight;

    float finishTime;
    public AudioSource audioSource;
    public AudioClip soundFootSteps;
    public AudioClip soundPatrol;
    public AudioClip soundChase;
    public AudioClip soundAttack;

    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject GetObjective()
    {
        return objective;
    }

    public GameObject GetTurret()
    {
        return turret;
    }

    public GameObject GetBullet()
    {
        return bullet;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    public AudioClip GetSoundPatrol()
    {
        return soundPatrol;
    }

    public AudioClip GetSoundChase()
    {
        return soundChase;
    }

    public AudioClip GetSoundAttack()
    {
        return soundAttack;
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objective = GameObject.FindGameObjectWithTag("Objective");
        turret = GameObject.FindGameObjectWithTag("Turret");
        //Bullet for turret is called in Attack state using Resources.Load
        anim = GetComponent<Animator>();
        ThisLineOfSight = GetComponent<LineOfSight>();

        finishTime = Time.time;
        audioSource = GetComponent<AudioSource>();
        soundFootSteps = (AudioClip)Resources.Load("Sounds/enemy_Footsteps");
        soundPatrol = (AudioClip)Resources.Load("Sounds/Patrol");
        soundChase = (AudioClip)Resources.Load("Sounds/Chase");
        soundAttack = (AudioClip)Resources.Load("Sounds/Attack");
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= finishTime)
        {
            finishTime = Time.time + soundFootSteps.length;
            audioSource.PlayOneShot(soundFootSteps);
        }

            //NOTE: This compares distance values between player and NPC. Need to chnage it so ic checks if player is in sight, if so then return true and have patrol change over to chase. 
            anim.SetFloat("animDistToPlayer", Vector3.Distance(transform.position, player.transform.position));

        if (ThisLineOfSight.CanSeeTarget == true)
        {
            anim.SetBool("animCanSeePlayer", true);
        }
        else
        {
            anim.SetBool("animCanSeePlayer", false);
            //anim.SetBool("awareOfPlayer", false); //Return to patrol state
        }

        if (ThisLineOfSight.playerNearby == true)
        {
            anim.SetBool("animPlayerNearby", true);
        }
        else
        {
            anim.SetBool("animPlayerNearby", false);
        }
    }

    // Debugging code to display a drawn line from the AI to whatever waypoint the AI is targeting.
    void OnDrawGizmosSelected()
    {

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.yellow, Color.yellow);
        }

        var path = nav.path;

        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }
}
